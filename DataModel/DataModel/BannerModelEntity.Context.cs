﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BannerEntities : DbContext
    {
        public BannerEntities()
            : base("name=BannerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BIG_Banners> BIG_Banners { get; set; }
        public virtual DbSet<BIG_Policy> BIG_Policy { get; set; }
        public virtual DbSet<BIG_About> BIG_About { get; set; }
        public virtual DbSet<BIG_Personnel> BIG_Personnel { get; set; }
        public virtual DbSet<BIG_AdminAccount> BIG_AdminAccount { get; set; }
        public virtual DbSet<BIG_Gallery> BIG_Gallery { get; set; }
        public virtual DbSet<BIG_Services> BIG_Services { get; set; }
        public virtual DbSet<BIG_Customer> BIG_Customer { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}