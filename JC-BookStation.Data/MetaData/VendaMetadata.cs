using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_BookStation.Data.MetaData
{
    internal class VendaMetadata
    {
        public int IdVenda { get; set; }
        public int? IdCliente { get; set; }
        public int? IdFuncionario { get; set; }
        [Required]
        public int? IdFinanceiroTipo { get; set; }
        [Required]
        public int? TipoVenda { get; set; }
        public int? Pago { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        [Required]
        public int? Parcelas { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataVenda { get; set; }
        public string StatusVenda { get; set; }
        public string Obs { get; set; }
    }
}
