using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_BookStation.Data.MetaData
{
    internal class ProdutosVendaMetadata
    {
        public int IdVendaItem { get; set; }
        public int? IdVenda { get; set; }
        public int? IdProduto { get; set; }
        public int? IdFuncionario { get; set; }
        public int? Quantidade { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorUnitario { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Bonus { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? SubTotal { get; set; }
    }
}
