using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class LojaMetadata
    {
        public int IdLoja { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        [Required]
        public string Fantasia { get; set; }
        [Required]
        public string NomeContato { get; set; }
        [Required]
        public int? CEP { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public int? Numero { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public short? Cidade { get; set; }
        [Required]
        public short? Estado { get; set; }
        [Required]
        public string CaminhoLogo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataCadastro { get; set; }
        public bool? ATIVO { get; set; }
    }
}
