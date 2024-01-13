using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class ComissaoMetadata
    {
        public int idComissao { get; set; }
        public int? idCliente { get; set; }
        public int? idVenda { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataVenda { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataBaixa { get; set; }
        public int? Status { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorComissao { get; set; }
    }
}
