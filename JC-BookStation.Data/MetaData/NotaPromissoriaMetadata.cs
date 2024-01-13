using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC_BookStation.Data.MetaData
{
    internal class NotaPromissoriaMetadata
    {
        public int IdNota { get; set; }
        public int? IdCliente { get; set; }
        public int? IdVenda { get; set; }
        public int? Parcela { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Vencimento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        public string Status { get; set; }
    }
}
