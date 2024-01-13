//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venda
    {
        public Venda()
        {
            this.ProdutosVenda = new HashSet<ProdutosVenda>();
            this.Financeiro = new HashSet<Financeiro>();
        }
    
        public int IdVenda { get; set; }
        [Required]
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdFuncionario { get; set; }
        [Required]
        public Nullable<int> IdFinanceiroTipo { get; set; }
        public Nullable<int> TipoVenda { get; set; }
        public Nullable<int> Pago { get; set; }
        [Required]
        public Nullable<decimal> Valor { get; set; }
        public Nullable<int> Parcelas { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DataVenda { get; set; }
        public string StatusVenda { get; set; }
        public string Obs { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual ICollection<ProdutosVenda> ProdutosVenda { get; set; }
        public virtual ICollection<Financeiro> Financeiro { get; set; }
    }
}
