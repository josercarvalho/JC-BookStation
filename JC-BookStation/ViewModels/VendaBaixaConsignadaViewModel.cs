using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.Models;

namespace JC_BookStation.ViewModels
{
    public class VendaBaixaConsignadaViewModel
    {
        public int IdVenda { get; set; }
        public string Nome { get; set; }
        public string TipoVenda { get; set; }
        public string FormaPagamento { get; set; }
        public int Parcelas { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Valor { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DataVenda { get; set; }

        public List<ProdutosVenda> ProdutosVendas { get; set; }
    }
}