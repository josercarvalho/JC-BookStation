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
    
    public partial class Cidade
    {
        public int cod_cidade { get; set; }
        public short cod_estado { get; set; }
        public string nom_cidade { get; set; }
    
        public virtual Estado Estado { get; set; }
    }
}
