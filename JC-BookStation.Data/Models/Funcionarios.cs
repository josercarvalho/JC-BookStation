//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using JC_BookStation.Data.MetaData;

namespace JC_BookStation.Data.Models
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(FuncionarioMetadata))]
    public partial class Funcionarios
    {
        public int idFuncionario { get; set; }
        public Nullable<int> IdFuncao { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public Nullable<short> Cidade { get; set; }
        public Nullable<short> Estado { get; set; }
        public Nullable<System.DateTime> Nascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public Nullable<System.DateTime> DataAdmissao { get; set; }
        public Nullable<System.DateTime> DataDemissao { get; set; }
        public Nullable<decimal> Salario { get; set; }
        public Nullable<int> DiaPagamento { get; set; }
        public Nullable<int> Comissao { get; set; }
        public string Observacao { get; set; }
    
        public virtual Funcao Funcao { get; set; }
    }
}
