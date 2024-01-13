using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class ProdutosCompraMetadata
    {
        public int IdCompraItem { get; set; }
        public int? IdCompra { get; set; }
        public int? IdProduto { get; set; }
        public int? Quantidade { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorUnitario { get; set; }
    }
}
