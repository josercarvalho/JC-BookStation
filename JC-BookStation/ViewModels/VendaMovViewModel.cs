using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.Models;

namespace JC_BookStation.ViewModels
{
    public class VendaMovViewModel
    {
        public string Periodo { get; set; }
        public int? TotalVendas { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? TotalBonus { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? SubTotal { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? ValorTotal { get; set; }
        public List<Venda> Vendas { get; set; }
    }
}