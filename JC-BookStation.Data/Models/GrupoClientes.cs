//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JC_BookStation.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GrupoClientes
    {
        public GrupoClientes()
        {
            this.Clientes = new HashSet<Clientes>();
        }
    
        public int IdGrupo { get; set; }
        public string Nome { get; set; }
    
        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
