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

    [MetadataType(typeof(ClientesMetadata))]
    public partial class Clientes
    {
        public Clientes()
        {
            this.Comissao = new HashSet<Comissao>();
            this.Orcamentos = new HashSet<Orcamentos>();
            this.Venda = new HashSet<Venda>();
            this.NotaPromissoria = new HashSet<NotaPromissoria>();
        }
    
        public int IdCliente { get; set; }
        public Nullable<int> IdGrupo { get; set; }
        public string IdIndica { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string InscEstadual { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public Nullable<short> Cidade { get; set; }
        public Nullable<short> Estado { get; set; }
        public Nullable<System.DateTime> Nascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Contato { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public string Observacao { get; set; }
        public Nullable<bool> Bloqueado { get; set; }
        public Nullable<bool> Excluido { get; set; }
    
        public virtual GrupoClientes GrupoClientes { get; set; }
        public virtual ICollection<Comissao> Comissao { get; set; }
        public virtual ICollection<Orcamentos> Orcamentos { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
        public virtual ICollection<NotaPromissoria> NotaPromissoria { get; set; }
    }
}
