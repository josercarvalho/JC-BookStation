using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.Models;

namespace JC_BookStation.ViewModels
{
    public class CompraViewModel
    {
        public CompraViewModel()
        {
            this.ProdutosCompra = new HashSet<ProdutosCompra>();
        }
    
        public int IdCompra { get; set; }
        public Nullable<int> IdFornecedor { get; set; }
        public string CodigoNota { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DataCompra { get; set; }
        public string Obs { get; set; }
        public Nullable<int> Parcelas { get; set; }
    
        public virtual Fornecedores Fornecedores { get; set; }
        public virtual ICollection<ProdutosCompra> ProdutosCompra { get; set; }
    }
}