//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JC_BookStation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProdutosVenda
    {
        public int IdVendaItem { get; set; }
        public Nullable<int> IdVenda { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<int> IdFuncionario { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
    
        public virtual Produtos Produtos { get; set; }
        public virtual Venda Venda { get; set; }
    }
}
