using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.Models;

namespace JC_BookStation.ViewModels
{
    public sealed class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            ProdutosCompra = new HashSet<ProdutosCompra>();
            ProdutosOrcamento = new HashSet<ProdutosOrcamento>();
            ProdutosVenda = new HashSet<ProdutosVenda>();
        }
    
        public int IdProduto { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        public string Nomecupom { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0,c}")]
        public decimal? ValorVenda { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0,c}")]
        public decimal? ValorCompra { get; set; }
        [Required]
        public int? Lucro { get; set; }
        [Required]
        public int? Estoque { get; set; }
        [DisplayFormat(DataFormatString = "{0,c}")]
        public decimal? ValorPrazo { get; set; }
        [Required]
        public int? EstoqueMinimo { get; set; }
        public string Observacao { get; set; }
        public string CodigoBarras { get; set; }
        public string UnCompra { get; set; }
        public string UnVenda { get; set; }
        public int? Fator { get; set; }
        public int? IdGrupo { get; set; }
        public bool? Lote { get; set; }
        public bool? Desativado { get; set; }
        public string NomeFoto { get; set; }
        public byte[] AnexoBytes { get; set; }
        public string AnexoExtensao { get; set; }
        public string AnexoTipo { get; set; }
    
        public GrupoProdutos GrupoProdutos { get; set; }
        public ICollection<ProdutosCompra> ProdutosCompra { get; set; }
        public ICollection<ProdutosOrcamento> ProdutosOrcamento { get; set; }
        public ICollection<ProdutosVenda> ProdutosVenda { get; set; }
    }
}