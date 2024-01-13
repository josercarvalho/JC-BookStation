using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Data.MetaData
{
    internal class FornecedorMetadata
    {
        public int IdFornecedor { get; set; }
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
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string Celular { get; set; }
        public string RGIE { get; set; }
    }
}
