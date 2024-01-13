using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class ProdutoMetadata
    {
        public int IdProduto { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Nomecupom { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorVenda { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorCompra { get; set; }
        public int? Lucro { get; set; }
        public int? Estoque { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorPrazo { get; set; }
        [Required]
        public int? EstoqueMinimo { get; set; }
        public string Observacao { get; set; }
        [Required]
        public long? CodigoBarras { get; set; }
        public string UnCompra { get; set; }
        public string UnVenda { get; set; }
        public int? Fator { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Comissao { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdFornecedor { get; set; }
        public bool AtivaSite { get; set; }
        public string NomeFoto { get; set; }
    }
}
