//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pap2020.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bloqueio
    {
        public System.DateTime dia { get; set; }
        public int is_locked { get; set; }
        public int id_relatorio { get; set; }
    
        public virtual Relatorio Relatorio { get; set; }
    }
}
