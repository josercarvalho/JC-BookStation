using System;
using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class ClientesMetadata
    {
        public int IdCliente { get; set; }
        public int? IdGrupo { get; set; }
        public string IdIndica { get; set; }
        [Required]
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        [Required]
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
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Nascimento { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Contato { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataCadastro { get; set; }
        public string Observacao { get; set; }
        public bool? Bloqueado { get; set; }
        public bool? Excluido { get; set; }
    }
}
