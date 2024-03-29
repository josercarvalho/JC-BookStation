//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.MetaData;

namespace JC_BookStation.Data.Models
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(FinanceiroMetadata))]
    public partial class Financeiro
    {
        public int IdFinanceiro { get; set; }
        public Nullable<int> IdVenda { get; set; }
        public Nullable<int> IdCompra { get; set; }
        public Nullable<int> IdFinanceiroTipo { get; set; }
        public Nullable<int> Parcelas { get; set; }
        public Nullable<int> Pago { get; set; }
        public Nullable<System.DateTime> Vencimento { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public string Obs { get; set; }
    
        public virtual Compra Compra { get; set; }
        public virtual FinanceiroTipo FinanceiroTipo { get; set; }
        public virtual Venda Venda { get; set; }
    }
}
