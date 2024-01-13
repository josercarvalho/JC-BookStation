using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.Models;

namespace JC_BookStation.ViewModels
{
    public sealed class VendaViewModel
    {
        public Venda Venda { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorTotal { get; set; }
        public string Nome { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataVenda { get; set; }
        public int? TotalItens { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? TotalBonus { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? SubTotal { get; set; }
        public DateTime DataExtenso { get; set; }
        public List<ProdutosVenda> ProdutosVendas { get; set; }
    }
}