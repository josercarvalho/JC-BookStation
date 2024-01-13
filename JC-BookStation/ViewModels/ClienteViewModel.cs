using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JC_BookStation.ViewModels
{
    public sealed class ClientesVM
    {
        public int IdCliente { get; set; }
        public int? IdGrupo { get; set; }
        public string IdIndica { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string NomeFantasia { get; set; }
        [Required]
        [CustomValidation.CustomValidationCpf(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }
        public string InscEstadual { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public short? Cidade { get; set; }
        [Required]
        public short? Estado { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Nascimento { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        [Remote("IsExistEmail", "Cadastro")]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Celular { get; set; }
        [Required]
        public string Contato { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataCadastro { get; set; }
        public string Observacao { get; set; }
        public bool? Bloqueado { get; set; }
        public bool? Excluido { get; set; }
    }
}