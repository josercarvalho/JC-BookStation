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

    [MetadataType(typeof(ProdutosOrcamentoMetadata))]
    public partial class ProdutosOrcamento
    {
        public int Id { get; set; }
        public int IdOrcamento { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
    
        public virtual Orcamentos Orcamentos { get; set; }
        public virtual Produtos Produtos { get; set; }
    }
}
