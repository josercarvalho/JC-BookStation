//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace JC_BookStation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public Nullable<int> IdPermissao { get; set; }
        public Nullable<int> IdLoja { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email v�lido...")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Senha { get; set; }
    
        public virtual Loja Loja { get; set; }
    }
}
