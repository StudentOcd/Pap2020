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
    
    public partial class Relatorio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Relatorio()
        {
            this.Dia = new HashSet<Dia>();
            this.Falta = new HashSet<Falta>();
            this.Bloqueio = new HashSet<Bloqueio>();
        }
    
        public int id_relatorio { get; set; }
        public string nome_empresa { get; set; }
        public string NIF { get; set; }
        public string email_empresa { get; set; }
        public int id_aluno { get; set; }
        public int id_professor { get; set; }
        public int id_monitor { get; set; }
        public Nullable<double> avaliacao { get; set; }
        public int is_locked { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dia> Dia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Falta> Falta { get; set; }
        public virtual Utilizador Utilizador { get; set; }
        public virtual Utilizador Utilizador1 { get; set; }
        public virtual Utilizador Utilizador2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloqueio> Bloqueio { get; set; }
    }
}
