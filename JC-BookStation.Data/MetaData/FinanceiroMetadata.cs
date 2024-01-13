using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class FinanceiroMetadata
    {
        public int IdFinanceiro { get; set; }
        public Nullable<int> IdVenda { get; set; }
        public Nullable<int> IdCompra { get; set; }
        public Nullable<int> IdFinanceiroTipo { get; set; }
        public Nullable<int> Parcelas { get; set; }
        public Nullable<int> Pago { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Vencimento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public Nullable<decimal> Valor { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public string Obs { get; set; }
    }
}
