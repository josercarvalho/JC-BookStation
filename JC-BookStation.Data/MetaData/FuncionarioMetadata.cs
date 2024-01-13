using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class FuncionarioMetadata
    {
        public int idFuncionario { get; set; }
        public int? IdFuncao { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public short? Cidade { get; set; }
        [Required]
        public short? Estado { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Nascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        public string RG { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        public string Celular { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataAdmissao { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataDemissao { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal? Salario { get; set; }
        [Required]
        public int? DiaPagamento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public int? Comissao { get; set; }
        public string Observacao { get; set; }
    }
}
