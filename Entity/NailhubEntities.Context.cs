﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Infrastructure.Repository;
    
    public partial class NailhubsEntities : MyContext
    {
        public NailhubsEntities()
            : base("name=NailhubsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<COUNTRY> COUNTRies { get; set; }
        public virtual DbSet<CURRENT_PRODUCT> CURRENT_PRODUCT { get; set; }
        public virtual DbSet<CURRENT_SERVICE> CURRENT_SERVICE { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<LOCATION> LOCATIONs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCTDETAIL> PRODUCTDETAILs { get; set; }
        public virtual DbSet<PROVINCE> PROVINCEs { get; set; }
        public virtual DbSet<ROLE> ROLEs { get; set; }
        public virtual DbSet<SERVICE> SERVICEs { get; set; }
        public virtual DbSet<SERVICEDETAIL> SERVICEDETAILs { get; set; }
        public virtual DbSet<SITE> SITEs { get; set; }
        public virtual DbSet<SITETYPE> SITETYPEs { get; set; }
        public virtual DbSet<STATE> STATEs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<THEME> THEMEs { get; set; }
        public virtual DbSet<THEMEDETAIL> THEMEDETAILs { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<TYPE> TYPEs { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<USER_ROLE> USER_ROLE { get; set; }
        public virtual DbSet<USERDETAIL> USERDETAILs { get; set; }
        public virtual DbSet<USERTYPE> USERTYPEs { get; set; }
    }
}
