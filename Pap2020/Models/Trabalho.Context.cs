﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SistemaGestaoEntities : DbContext
    {
        public SistemaGestaoEntities()
            : base("name=SistemaGestaoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Dia> Dia { get; set; }
        public virtual DbSet<Falta> Falta { get; set; }
        public virtual DbSet<Relatorio> Relatorio { get; set; }
        public virtual DbSet<Tipo_Utilizador> Tipo_Utilizador { get; set; }
        public virtual DbSet<Utilizador> Utilizador { get; set; }
        public virtual DbSet<Bloqueio> Bloqueio { get; set; }
        public virtual DbSet<Avaliação> Avaliação { get; set; }
    }
}
