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
    
    public partial class Orcamentos
    {
        public Orcamentos()
        {
            this.ProdutosOrcamento = new HashSet<ProdutosOrcamento>();
        }
    
        public int IdOrcamento { get; set; }
        [Required]
        public Nullable<int> IdCliente { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Dia { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Validade { get; set; }
        [Required]
        public Nullable<decimal> Valor { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual ICollection<ProdutosOrcamento> ProdutosOrcamento { get; set; }
    }
}
