//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JC_BookStation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pais
    {
        public Pais()
        {
            this.Estado = new HashSet<Estado>();
        }
    
        public short cod_pais { get; set; }
        public string sgl_pais { get; set; }
        public string nom_pais { get; set; }
    
        public virtual ICollection<Estado> Estado { get; set; }
    }
}
