var urlPath = window.location.pathname;

$(function () {
    //$('input.dinheiro').mask('#.##0,00', { reverse: true });
    $("input.dinheiro").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
    $("#DataCompra").mask("99/99/9999");
    //$('#DataCompra').datepicker({
    //    //showOn: "button",
    //    //buttonImage: '/images/icon-calendar.gif',
    //    //buttonImageOnly: true,
    //    //changeMonth: true,
    //    //changeYear: true,
    //    closeText: 'Fechar',
    //    prevText: '&#x3c;Anterior',
    //    nextText: 'Pr&oacute;ximo&#x3e;',
    //    currentText: 'Hoje',
    //    monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
    //    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
    //    dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
    //    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
    //    dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
    //    weekHeader: 'Sm',
    //    dateFormat: 'dd/mm/yy',
    //    firstDay: 0,
    //    isRTL: false,
    //    showMonthAfterYear: false,
    //    yearSuffix: ''
    //});

    var viewModel = new CompraModel();
    ko.applyBindings(viewModel);
});

function formatCurrency(value) {
    return "R$ " + value.toFixed(2);
}

function CompraModel() {
    var self = this;

    // Compra
    self.IdCompra = ko.observable();
    self.CodigoNota = ko.observable();
    self.IdFornecedor = ko.observableArray([]);
    self.DataCompra = ko.observable();
    self.ValorTotal = ko.observable().dinheiro();

    // Financeiro
    self.IdFinanceiroTipo = ko.observableArray([]);
    self.Parcelas = ko.observable(1);
    self.Vencimento = ko.observable();
    self.Obs = ko.observable();

    // Produtos da compra
    self.IdProduto = ko.observable();
    self.CodigoBarras = ko.observable();
    self.Nome = ko.observable();
    self.Quantidade = ko.observable(1);
    //self.ValorUnitario = ko.observable().dinheiro();
    self.ValorUnitario = ko.observable();
    self.subtotal = ko.computed(function () {
        return self.ValorUnitario() ? parseFloat("0" + self.ValorUnitario()) * parseInt("0" + self.Quantidade(), 10) : 0;
    });

    var compra = {
        IdCompra: self.IdCompra,
        CodigoNota: self.CodigoNota,
        IdFornecedor: self.IdFornecedor,
        DataCompra: self.DataCompra,
        ValorTotal: self.ValorTotal
    };

    var financeiro = {
        IdFinanceiroTipo: self.IdFinanceiroTipo,
        Parcelas: self.Parcelas,
        Vencimento: self.Vencimento,
        ValorParcela: self.ValorTotal,
        Obs: self.Obs
    };

    var produto = {
        IdProduto: self.IdProduto,
        CodigoBarras: self.CodigoBarras,
        Nome: self.Nome,
        Quantidade: self.Quantidade,
        ValorUnitario: self.ValorUnitario,
        subtotal: self.subtotal
    };

    self.compra = ko.observable();
    self.financeiro = ko.observable();
    self.produto = ko.observable();

    // Lista dos Produtos
    self.listaProdutos = ko.observableArray();

    self.CodigoNota.subscribe(function () {
        $("#progress").show();
        if (self.CodigoNota() != "") {
            $.getJSON('BuscaNotaFiscal/', { CodigoNota: self.CodigoNota },
                function (data) {
                    if (data != "1") {
                        bootbox.alert('Nota Fiscal já CADASTRADA. Verifique número da Nota Fiscal.');
                        self.CodigoNota('');
                    }
                }).fail(function (xhr, textStatus, err) { bootbox.alert(err); });
        }
        $("#progress").hide();
    });

    //Busca Codigo e ValorUnitario do Produto pelo Código
    produto.CodigoBarras.subscribe(function () {
        $("#progress").show();
        if (produto.CodigoBarras() != "") {
            $.getJSON('LoadProdutoId/', { produtoId: produto.CodigoBarras },
                    function (data) {
                        produto.IdProduto(data.IdProduto);
                        produto.Nome(data.Nome);
                        produto.ValorUnitario(data.ValorCompra);
                        $("#Quantidade").focus();
                    }).fail(function (xhr, textStatus, err) { bootbox.alert(err); });
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
            self.listaProdutos.push({ IdProduto: produto.IdProduto(), CodigoBarras: produto.CodigoBarras(), Nome: produto.Nome(), ValorUnitario: produto.ValorUnitario(), Quantidade: produto.Quantidade(), subtotal: produto.subtotal() });
            //self.listaproduto.push(produtos);
            self.limpar();
        } else {
            bootbox.alert('Por favor preencha todos os campos !!');
        }
        $('#CodigoBarras').focus();
    };

    self.salvarProduto = function () {
        
        if (this.listaProdutos().length == 0) {
            bootbox.alert('Por favor, adicione produtos à compra !!');
            return;
        }

        if (self.IdFornecedor() == 0 && self.DataCompra == "" && self.IdFinanceiroTipo == 0 && self.Parcelas == "" && self.CodigoNota == "") {
            bootbox.alert('Por favor, preencha os dados da Compra corretamente !!');
            return;
        }

        $("#progress").show();

        var valorTotal = formatCurrency(parseFloat($('#ValorTotal').val().replace('.', '').replace(',', '.')));
        var total = self.grandTotal();
        //bootbox.alert('valor ' + valorTotal + ' total ' + total);
        if (valorTotal != total) {
            bootbox.alert('O valor da Nota Fiscal não bate com valor total de produto. Verifique...!!');
            $("#progress").hide();
            return;
        }

        var dadoscompra = ko.toJS(compra);
        var dadosfinanceiro = ko.toJS(financeiro);
        var produtoscompra = ko.toJS(this.listaProdutos);

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: 'SalvarProdutos/',
            data: JSON.stringify({ compra: dadoscompra, financeiro: dadosfinanceiro, produtoscompra: produtoscompra }),
            traditional: true,
            success: self.sucesso,
            error: self.falha,
            complete: self.reset()
        }).fail(function (xhr, textStatus, err) {
            alert(err);
        });

        $("#progress").hide();
    };

    self.sucesso = function () { bootbox.alert("Dados salvos com sucesso!"); };
    self.falha = function () { bootbox.alert("Falha ao salvar os dados!"); };

    self.limpar = function () {
        self.CodigoBarras("");
        self.Nome("");
        self.Quantidade(1);
        self.ValorUnitario("");
        self.subtotal("");
    };

    self.excluir = function (item) {
        self.listaProdutos.remove(item);
    };

    self.reset = function () {
        self.IdCompra('');
        self.CodigoNota('');
        self.DataCompra('');
        self.ValorTotal(0);
        self.Parcelas(1);
        self.Vencimento('');
        self.CodigoBarras('');
        self.Obs('');
        self.IdProduto('');
        self.Nome('');
        self.Quantidade(1);
        self.ValorUnitario('');
        self.listaProdutos([]);
    };
};
