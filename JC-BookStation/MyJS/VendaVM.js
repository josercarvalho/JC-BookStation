var urlPath = window.location.pathname;

$(document).ready(function () {
    $("input.dinheiro").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
    $('#DataVenda').mask("99/99/9999");

    $("#NomeDoProduto").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete"), minLength: 1 });
    });

    $('.modal').modal({
        show: false,
        keyboard: false,
        backdrop: 'static'
    });

    var viewModel = new VendaModel();
    ko.applyBindings(viewModel);
});

function formatCurrency(value) {
    return "R$ " + value.toFixed(2);
}

function VendaModel() {
    var self = this;

    // Venda
    //self.IdCliente = $('#IdCliente').val();
    self.idVenda= ko.observable();
    self.TipoVenda = ko.observable();
    self.DataVenda = ko.observable();
    self.Valor = ko.observable();

    // Financeiro
    self.IdFinanceiroTipo = ko.observable();
    self.Parcelas = ko.observable(1);
    self.Vencimento = ko.observable();

    // Produtos da compra
    self.IdProduto = ko.observable();
    self.CodigoBarras = ko.observable();
    self.Nome = ko.observable();
    self.Quantidade = ko.observable(1);
    self.Comissao = ko.observable(self.itemBonus);
    self.Estoque = ko.observable(0);
    self.ValorUnitario = ko.observable();
    self.subtotal = ko.computed(function () {
        return self.ValorUnitario() ? parseFloat("0" + self.ValorUnitario()) * parseInt("0" + self.Quantidade(), 10) : 0;
    });
    var produto = {
        IdProduto: self.IdProduto,
        CodigoBarras: self.CodigoBarras,
        Nome: self.Nome,
        Quantidade: self.Quantidade,
        Comissao: self.Comissao,
        Estoque: self.Estoque,
        ValorUnitario: self.ValorUnitario,
        subtotal: self.subtotal
    };

    self.produto = ko.observable();

    // Lista dos Produtos
    self.listaProdutos = ko.observableArray();

    //Busca Codigo e ValorUnitario do Produto pelo Código
    produto.CodigoBarras.subscribe(function () {
        $("#progress").show();
        var id = produto.CodigoBarras;
        //var strUrl = '@Url.Action("BuscaProduto", "Venda")';
        if (produto.CodigoBarras() != "") {
            $.getJSON("/Admin/Venda/BuscaProduto/", { "codigoBarras": id }, function (data) {
                    if (data != null) {
                        produto.IdProduto(data.IdProduto);
                        produto.Nome(data.Nome);
                        produto.ValorUnitario(data.ValorVenda);
                        produto.Comissao(data.Comissao);
                        produto.Estoque(data.Estoque);
                        $("#Quantidade").focus();
                    } else {
                        bootbox.alert('ATENÇÃO! Produto não CADASTRADO.');
                        self.limpar();
                    }
                });
        }
        $("#progress").hide();
    });

    // Calcula Quantidade Total
    self.itemTotal = ko.computed(function () {
        var sum = 0;
        var arr = self.listaProdutos();
        for (var i = 0; i < arr.length; i++) {
            sum += parseInt(arr[i].Quantidade);
        }
        return sum;
    });

    // Calcula Bonus Total
    self.itemBonus = ko.computed(function () {
        var sum = 0;
        var arr = self.listaProdutos();
        for (var i = 0; i < arr.length; i++) {
            sum += (parseInt(arr[i].Comissao) * parseInt(arr[i].Quantidade));
        }
        return formatCurrency(sum);
    });

    // Calcula Total
    self.grandTotal = ko.computed(function () {
        var sum = 0.0;
        var arr = self.listaProdutos();
        for (var i = 0; i < arr.length; i++) {
            sum += arr[i].subtotal;
        }
        return formatCurrency(sum);
    });

    self.adcionarItem = function () {
        if (produto.CodigoBarras() != "" && produto.Nome() != "" && produto.ValorUnitario() != "" && produto.subtotal() != 0) {
            if (produto.Estoque() >= self.Quantidade()) {
                self.listaProdutos.push({ IdProduto: produto.IdProduto(), CodigoBarras: produto.CodigoBarras(), Nome: produto.Nome(), Comissao: produto.Comissao(), ValorUnitario: produto.ValorUnitario(), Quantidade: produto.Quantidade(), subtotal: produto.subtotal() });
                self.limpar();
                $("'#CodigoBarras").focus();
            } else {
                bootbox.alert('Produto com saldo insuficiente em ESTOQUE para esta venda.');
                self.limpar();
            }
        } else {
            bootbox.alert('Por favor preencha todos os campos !!');
        }
    };

    self.salvarProduto = function () {
        $("#progress").show();

        if (this.listaProdutos().length == 0) {
            bootbox.alert('Por favor, adicione produtos à venda.');
            $("#progress").hide();
            return;
        }
        
        if (self.IdFinanceiroTipo == "" && self.Valor == "" && self.Parcelas == 0 && self.DataVenda == "") {
            bootbox.alert('Por favor, preencha os dados da Venda corretamente.');
            $("#progress").hide();
            return;
        }

        if ($("#IdFinanceiroTipo").val() < 1 && $("#Valor").val() == "R$ 0,00" && $("#Parcelas").val() == 0 && $("#DataVenda").val() == "") {
            bootbox.alert('Por favor, preencha os dados da Venda corretamente.');
            $("#progress").hide();
            return;
        }

        var dadosVenda = ko.toJS({
            TipoVenda: self.TipoVenda,
            IdFinanceiroTipo: self.IdFinanceiroTipo,
            Parcelas: self.Parcelas,
            DataVenda: $('#DataVenda').val(), // self.DataVenda,
            Valor: $('#Valor').val()
        });
        var dadosfinanceiro = ko.toJS({
            IdFinanceiroTipo: self.IdFinanceiroTipo,
            Parcelas: self.Parcelas,
            Vencimento: $('#DataVenda').val(),
            ValorParcela: $('#Valor').val()
        });
        var listaprodutos = ko.toJS(this.listaProdutos);
        //var strUrl = '@Url.Action("SalvarProdutos", "Venda")';
        $.ajax({
            url: "/Admin/Venda/SalvarProdutos/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ venda: dadosVenda, financeiro: dadosfinanceiro, produtosVenda: listaprodutos }),
            success: function () {
                self.idVenda = "@ViewBag.Id_Venda";
                bootbox.alert("Dados salvos com sucesso!");
            },
            error: self.falha,
            complete: function () {
                //self.HabilitaBotoes();
                $("#btn-Imaprimir").removeClass("disabled");
                $("#btnSalvar").addClass("disabled");
                $("#progress").hide();
            }
        }).fail(function (xhr, textStatus, err) {
            bootbox.alert(err);
        });
        $("#progress").hide();
    };

    self.sucesso = function () {
        if (self.IdFinanceiroTipo == 5) {
            bootbox.dialog({
                message: "Deseja finalizar ou imprimir Nota Promissória",
                buttons: {
                    success: {
                        label: "Finalizar",
                        className: "btn-success",
                        callback: function () {
                            Example.show("Dados salvos com sucesso!");
                        }
                    }
                },
                main: {
                    label: "Nota Promissória",
                    className: "btn-primary",
                    callback: function () {
                        Example.show("Imprimindo Nota Promissória");
                        var id = window.ViewBag.idVenda;
                        NotaPromissoria(id);
                    }
                }
            });
        } else {
            bootbox.alert("Dados salvos com sucesso!");
        }
    };

    self.falha = function () { bootbox.alert("Falha ao salvar os dados!"); };

    self.NotaPromissoria = function (id) {
        $.post("/Admin/Venda/NotaPromissoria/", id, function () {
            Example.show("Nota Promissória Impressa com sucesso!");
        });
    };

    self.limpar = function () {
        self.IdProduto("");
        self.CodigoBarras("");
        self.Nome("");
        self.Quantidade(1);
        self.Comissao("");
        self.ValorUnitario("");
        self.subtotal("");
    };

    self.excluir = function (item) {
        self.listaProdutos.remove(item);
    };

    self.reset = function () {
        //self.idCliente('');
        self.DataVenda('');
        self.Valor(0);
        self.Parcelas(1);
        self.Vencimento('');
        self.IdProduto('');
        self.CodigoBarras('');
        self.Nome('');
        self.Quantidade(1);
        self.ValorUnitario('');
        self.Comissao('');
        self.listaProdutos([]);
        $("#btn-Imaprimir").addClass("disabled");
        $("#btn-Promissoria").addClass("disabled");
        bootbox.alert("Dados salvos com sucesso!");
    };

    self.HabilitaBotoes = function () {
        $("#btn-Imaprimir").removeClass("disabled");
        $("#btn-Promissoria").removeClass("disabled");
    };

    //self.printProdutos = function () {
    //    var itemId = $(this).data('id');
    //    var nome = $(this).data("nome");
    //    bootbox.confirm("Confirma Impressão do Relatório de Venda", function (result) {
    //        if (result) {
    //            excluirCliente(itemId);
    //            setTimeout("Refresh()", 3000);
    //        }
    //    });
    //};

    //self.printPromissoria = function () {
    //    var itemId = $(this).data('id');
    //    var nome = $(this).data("nome");
    //    bootbox.prompt("Entre com a data do Vencimento da Promissória", function (result) {
    //        if (result === null) {
    //            Example.show("Promissória <b> CANCELADA </b>");
    //        } else {
    //            window.open();
    //        }
    //    });
    //}
}

//function Venda(Venda) {
//    var self = this;
//    self.IdVenda = ko.observable(Venda.IdVenda || '');
//    self.IdCliente = ko.observable(Venda.IdCliente || '');
//    self.IdFuncionario = ko.observableArray(Venda.IdFuncionario || []);
//    self.IdFinanceiroTipo = ko.observableArray(Venda.IdFinanceiroTipo || []);
//    self.TipoVenda = ko.observableArray(Venda.TipoVenda || []);
//    self.Pago = ko.observableArray(Venda.Pago || []);
//    self.Valor = ko.observable(Venda.Valor || '');
//    self.Parcelas = ko.observableArray(Venda.Parcelas || []);
//    self.DataVenda = ko.observableArray(Venda.DataVenda || []);
//    self.StatusVenda = ko.observable(Venda.StatusVenda || '');
//    self.Obs = ko.observable(Venda.Obs || '');
//    self.Financeiro = ko.observableArray(Venda.Financeiro || []);
//    self.ProdutosVenda = ko.observableArray(Venda.ProdutosVenda || []);
//}

//function Financeiro(financeiro) {
//    var self = this;
//    self.idFinanceiro = ko.observable(financeiro.idFinanceiro || '');
//    self.idVenda = ko.observableArray(financeiro.idVenda || []);
//    self.idCompra = ko.observableArray(financeiro.idCompra || []);
//    self.idFinanceiroTipo = ko.observableArray(financeiro.idFinanceiroTipo || []);
//    self.parcelas = ko.observableArray(financeiro.parcelas || []);
//    self.pago = ko.observableArray(financeiro.pago || []);
//    self.vencimento = ko.observableArray(financeiro.vencimento || []);
//    self.valor = ko.observableArray(financeiro.valor || []);
//    self.dataPagamento = ko.observableArray(financeiro.dataPagamento || []);
//    self.obs = ko.observable(financeiro.obs || '');
//    self.compra = ko.observable(financeiro.compra || '');
//    self.financeiroTipo = ko.observable(financeiro.financeiroTipo || '');
//    self.venda = ko.observable(financeiro.venda || '');
//}
