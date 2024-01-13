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
    
    public partial class Compra
    {
        public Compra()
        {
            this.ProdutosCompra = new HashSet<ProdutosCompra>();
            this.Financeiro = new HashSet<Financeiro>();
        }
    
        public int IdCompra { get; set; }
        [Required]
        public Nullable<int> IdFornecedor { get; set; }
        [Required]
        public string CodigoNota { get; set; }
        [Required]
        public Nullable<decimal> ValorTotal { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DataCompra { get; set; }
        public string Obs { get; set; }
        public Nullable<int> Parcelas { get; set; }
    
        public virtual Fornecedores Fornecedores { get; set; }
        public virtual ICollection<ProdutosCompra> ProdutosCompra { get; set; }
        public virtual ICollection<Financeiro> Financeiro { get; set; }
    }
}