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
    using Generic.Infrastructure.Repositories;
    
    public partial class NailhubsEntities : MyContext
    {
        public NailhubsEntities()
            : base("name=AppContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<COUNTRY> COUNTRies { get; set; }
        public virtual DbSet<CURRENT_PRODUCT> CURRENT_PRODUCT { get; set; }
        public virtual DbSet<CURRENT_SERVICE> CURRENT_SERVICE { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<LOCATION> LOCATIONs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCT_DETAIL> PRODUCT_DETAIL { get; set; }
        public virtual DbSet<SERVICE> SERVICEs { get; set; }
        public virtual DbSet<SERVICE_DETAIL> SERVICE_DETAIL { get; set; }
        public virtual DbSet<SITE> SITEs { get; set; }
        public virtual DbSet<SITE_TYPE> SITE_TYPE { get; set; }
        public virtual DbSet<STATE> STATEs { get; set; }
        public virtual DbSet<THEME> THEMEs { get; set; }
        public virtual DbSet<THEME_DETAIL> THEME_DETAIL { get; set; }
        public virtual DbSet<TITLE> TITLEs { get; set; }
        public virtual DbSet<TYPE> TYPEs { get; set; }
        public virtual DbSet<USER_DETAIL> USER_DETAIL { get; set; }
        public virtual DbSet<USER_TYPE> USER_TYPE { get; set; }
    }
}
