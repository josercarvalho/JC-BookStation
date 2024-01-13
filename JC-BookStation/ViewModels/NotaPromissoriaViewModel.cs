using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.ViewModels
{
    public class NotaPromissoriaViewModel
    {
        public int IdNota { get; set; }
        public string Nome { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Valor { get; set; }
        public string ValorExtenso { get; set; }
        public string CPF { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Vencimento { get; set; }
        public string NomeEmpresa { get; set; }
        public string CNPJEmpresa { get; set; }
        public string EnderecoEmpresa { get; set; }

    }
}