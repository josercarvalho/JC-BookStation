function Orcamentos(orcamentos) {
    var self = this;
    self.IdOrcamento = orcamentos.IdOrcamento;
    self.IdCliente = orcamentos.IdCliente;
    self.Dia = orcamentos.Dia;
    self.Validade = orcamentos.Validade;
    self.Valor = orcamentos.Valor;
    self.Clientes = ko.observable(orcamentos.Clientes || '');
    self.ProdutosOrcamento = ko.observableArray(orcamentos.ProdutosOrcamento || []);
}

