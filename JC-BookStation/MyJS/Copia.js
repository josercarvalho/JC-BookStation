

// Venda Knockout

//<script type="text/javascript">

        $(function () {
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
            //self.idVenda = ko.observable();
            self.idCliente = ko.observable();
            self.tipoVenda = ko.observableArray([]);
            self.dataVenda = ko.observable();
            self.Valor = ko.observable(self.grandTotal); // ko.computed(function () { return self.grandTotal(); });

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
            self.Comissao = ko.observable(self.itemBonus);
            self.ValorUnitario = ko.observable();
            self.subtotal = ko.computed(function () {
                return self.ValorUnitario() ? parseFloat("0" + self.ValorUnitario()) * parseInt("0" + self.Quantidade(), 10) : 0;
            });

            //var dadosVenda = {
            //    idVenda: self.idVenda,
            //    idCliente: self.idCliente,
            //    tipoVenda: self.tipoVenda,
            //    dataVenda: self.dataVenda,
            //    Valor: self.Valor
            //};
            //var dadosFinanceiro = {
            //    IdFinanceiroTipo: self.IdFinanceiroTipo,
            //    Parcelas: self.Parcelas,
            //    Vencimento: self.dataVenda,
            //    ValorParcela: self.Valor,
            //    Obs: self.Obs
            //};
            var produto = {
                IdProduto: self.IdProduto,
                CodigoBarras: self.CodigoBarras,
                Nome: self.Nome,
                Quantidade: self.Quantidade,
                Comissao: self.Comissao,
                ValorUnitario: self.ValorUnitario,
                subtotal: self.subtotal
            };

            self.dadosVenda = ko.observable();
            self.dadosFinanceiro = ko.observable();
            self.produto = ko.observable();

            // Lista dos Produtos
            self.listaProdutos = ko.observableArray();

            //Busca Codigo e ValorUnitario do Produto pelo Código
            produto.CodigoBarras.subscribe(function () {
                $("#progress").show();
                var strUrl = '@Url.Action("BuscaProduto", "Venda")';
                if (produto.CodigoBarras() != "") {
                    $.getJSON(strUrl, { codigoBarras: produto.CodigoBarras },
                        function (data) {
                            produto.Nome(data.Nome);
                            produto.ValorUnitario(data.ValorVenda);
                            produto.Comissao(data.Comissao);
                            $("#Quantidade").focus();
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
                    self.listaProdutos.push({ IdProduto: produto.IdProduto(), CodigoBarras: produto.CodigoBarras(), Nome: produto.Nome(), Comissao: produto.Comissao(), ValorUnitario: produto.ValorUnitario(), Quantidade: produto.Quantidade(), subtotal: produto.subtotal() });
                    //self.listaproduto.push(produto);
                    self.limpar();
                    $("'#CodigoBarras").focus();
                } else {
                    bootbox.alert('Por favor preencha todos os campos !!');
                }
            };

            self.salvarProduto = function () {
                $("#progress").show();

                if (this.listaProdutos().length == 0) {
                    bootbox.alert('Por favor, adicione produtos à venda !!');
                    return;
                }

                if (self.idCliente && self.IdFinanceiroTipo == "" && self.Valor == "" && self.Parcelas == "" && self.dataVenda == "") {
                    bootbox.alert('Por favor, preencha os dados da Venda corretamente !!');
                    return;
                }

                var dadosvenda = ko.toJS({
                    idVenda: self.idVenda,
                    idCliente: self.idCliente,
                    tipoVenda: self.tipoVenda,
                    dataVenda: self.dataVenda,
                    Valor: self.Valor
                });
                var dadosfinanceiro = ko.toJS({
                    IdFinanceiroTipo: self.IdFinanceiroTipo,
                    Parcelas: self.Parcelas,
                    Vencimento: self.dataVenda,
                    ValorParcela: self.Valor,
                    Obs: self.Obs
                });
                var produtosvenda = ko.toJS(this.listaProdutos);
                var strUrl = '@Url.Action("SalvarProdutos", "Venda")';

                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: strUrl,
                    data: JSON.stringify({ venda: dadosvenda, financeiro: dadosfinanceiro, produtos: produtosvenda }),
                    traditional: true,
                    success: self.sucesso,
                    error: self.falha,
                    complete: self.reset()
                }).fail(function (xhr, textStatus, err) {
                    bootbox.alert(err);
                });

                $("#progress").hide();
            };

            self.sucesso = function () { bootbox.alert("Dados salvos com sucesso!"); };
            self.falha = function () { bootbox.alert("Falha ao salvar os dados!"); };

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
                //self.idVenda('');
                self.idCliente('');
                self.dataVenda('');
                self.Valor(0);
                self.Parcelas(1);
                self.Vencimento('');
                self.Obs('');
                self.IdProduto('');
                self.CodigoBarras('');
                self.Nome('');
                self.Quantidade(1);
                self.ValorUnitario('');
                self.Comissao('');
                self.listaProdutos([]);
            };
        }
    //</script>

// Compra Knockout

//<script type="text/javascript">

        $(function () {
            //$('input.dinheiro').mask('#.##0,00', { reverse: true });
            $("input.dinheiro").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
            $("#DataCompra").mask("99/99/9999");

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
            var strUrl = '@Url.Action("BuscaNotaFiscal", "Compra")';
            $.getJSON(strUrl, { CodigoNota: self.CodigoNota },
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
            var strUrl = '@Url.Action("LoadProdutoId", "Compra")';
            $.getJSON(strUrl, { produtoId: produto.CodigoBarras },
                    function (data) {
                        produto.IdProduto(data.IdProduto);
                        produto.Nome(data.Nome);
                        produto.ValorUnitario(data.ValorVenda);
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
        $("#progress").show();

        if (this.listaProdutos().length == 0) {
            bootbox.alert('Por favor, adicione produtos à compra !!');
            return;
        } else {
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
            var strUrl = '@Url.Action("SalvarProdutos", "Compra")';
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: strUrl,
                data: JSON.stringify({ dadoscompra: dadoscompra, dadosfinanceiro: dadosfinanceiro, produtoscompra: produtoscompra }),
                traditional: true,
                success: self.sucesso,
                error: self.falha,
                complete: self.reset()
            }).fail(function (xhr, textStatus, err) {
                alert(err);
            });
        }
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

// </script>