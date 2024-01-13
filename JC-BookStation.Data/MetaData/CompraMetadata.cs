using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class CompraMetadata
    {
        public int IdCompra { get; set; }
        [Required]
        public int? IdFornecedor { get; set; }
        [Required]
        public string CodigoNota { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorTotal { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataCompra { get; set; }
        public string Obs { get; set; }
        [Required]
        public int? Parcelas { get; set; }
    }
}
