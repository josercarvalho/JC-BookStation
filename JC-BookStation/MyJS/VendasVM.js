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
});

function formatCurrency(value) {
    return "R$ " + value.toFixed(2);
}

$(function() {

    function Venda(Venda) {
        var self = this;
        self.IdVenda = ko.observable(Venda.IdVenda || '');
        self.IdCliente = ko.observable(Venda.IdCliente || '');
        self.IdFuncionario = ko.observableArray(Venda.IdFuncionario || []);
        self.IdFinanceiroTipo = ko.observableArray(Venda.IdFinanceiroTipo || []);
        self.TipoVenda = ko.observableArray(Venda.TipoVenda || []);
        self.Pago = ko.observableArray(Venda.Pago || []);
        self.Valor = ko.observable(Venda.Valor || '');
        self.Parcelas = ko.observableArray(Venda.Parcelas || []);
        self.DataVenda = ko.observableArray(Venda.DataVenda || []);
        self.StatusVenda = ko.observable(Venda.StatusVenda || '');
        self.Obs = ko.observable(Venda.Obs || '');
        self.Financeiro = ko.observableArray(Venda.Financeiro || []);
        self.ProdutosVenda = ko.observableArray(Venda.ProdutosVenda || []);
    }

    function Financeiro(Financeiro) {
        var self = this;
        self.IdFinanceiro = ko.observable(Financeiro.IdFinanceiro || '');
        self.IdVenda = ko.observableArray(Financeiro.IdVenda || []);
        self.IdCompra = ko.observableArray(Financeiro.IdCompra || []);
        self.IdFinanceiroTipo = ko.observableArray(Financeiro.IdFinanceiroTipo || []);
        self.Parcelas = ko.observableArray(Financeiro.Parcelas || []);
        self.Pago = ko.observableArray(Financeiro.Pago || []);
        self.Vencimento = ko.observableArray(Financeiro.Vencimento || []);
        self.Valor = ko.observableArray(Financeiro.Valor || []);
        self.DataPagamento = ko.observableArray(Financeiro.DataPagamento || []);
        self.Obs = ko.observable(Financeiro.Obs || '');
    }

    function ProdutosVenda(ProdutosVenda) {
        var self = this;
        self.IdVendaItem = ko.observable(ProdutosVenda.IdVendaItem || '');
        self.IdVenda = ko.observableArray(ProdutosVenda.IdVenda || []);
        self.IdProduto = ko.observableArray(ProdutosVenda.IdProduto || []);
        self.IdFuncionario = ko.observableArray(ProdutosVenda.IdFuncionario || []);
        self.Quantidade = ko.observableArray(ProdutosVenda.Quantidade || []);
        self.ValorUnitario = ko.observableArray(ProdutosVenda.ValorUnitario || []);
        self.Bonus = ko.observableArray(ProdutosVenda.Bonus || []);
        self.SubTotal = ko.observableArray(ProdutosVenda.SubTotal || []);
    }

    

})