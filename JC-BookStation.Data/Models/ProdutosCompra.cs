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

    [MetadataType(typeof(ProdutosCompraMetadata))]
    public partial class ProdutosCompra
    {
        public int IdCompraItem { get; set; }
        public Nullable<int> IdCompra { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
    
        public virtual Compra Compra { get; set; }
        public virtual Produtos Produtos { get; set; }
    }
}
