﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VIVLIO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FSPCEntities : DbContext
    {
        public FSPCEntities()
            : base("name=FSPCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MATIERE> MATIERE { get; set; }
        public virtual DbSet<MESSAGE> MESSAGE { get; set; }
        public virtual DbSet<NIVEAU> NIVEAU { get; set; }
        public virtual DbSet<OFFER> OFFER { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
