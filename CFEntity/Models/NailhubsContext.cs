using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CFEntity.Models.Mapping;
using Generic.Infrastructure.Repositories;

namespace CFEntity.Models
{
    public partial class NailhubsContext : MyContext
    {
        static NailhubsContext()
        {
            Database.SetInitializer<NailhubsContext>(null);
        }

        public NailhubsContext()
            : base("Name=AppContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<CITY> CITies { get; set; }
        public DbSet<COUNTRY> COUNTRies { get; set; }
        public DbSet<CURRENT_PRODUCT> CURRENT_PRODUCT { get; set; }
        public DbSet<CURRENT_SERVICE> CURRENT_SERVICE { get; set; }
        public DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public DbSet<LOCATION> LOCATIONs { get; set; }
        public DbSet<PRODUCT> PRODUCTs { get; set; }
        public DbSet<PRODUCT_DETAIL> PRODUCT_DETAIL { get; set; }
        public DbSet<PROVINCE> PROVINCEs { get; set; }
        public DbSet<SERVICE> SERVICEs { get; set; }
        public DbSet<SERVICE_DETAIL> SERVICE_DETAIL { get; set; }
        public DbSet<SITE> SITEs { get; set; }
        public DbSet<SITE_TYPE> SITE_TYPE { get; set; }
        public DbSet<STATE> STATEs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<THEME> THEMEs { get; set; }
        public DbSet<THEME_DETAIL> THEME_DETAIL { get; set; }
        public DbSet<TITLE> TITLEs { get; set; }
        public DbSet<TYPE> TYPEs { get; set; }
        public DbSet<USER_DETAIL> USER_DETAIL { get; set; }
        public DbSet<USER_TYPE> USER_TYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new CITYMap());
            modelBuilder.Configurations.Add(new COUNTRYMap());
            modelBuilder.Configurations.Add(new CURRENT_PRODUCTMap());
            modelBuilder.Configurations.Add(new CURRENT_SERVICEMap());
            modelBuilder.Configurations.Add(new EMPLOYEEMap());
            modelBuilder.Configurations.Add(new LOCATIONMap());
            modelBuilder.Configurations.Add(new PRODUCTMap());
            modelBuilder.Configurations.Add(new PRODUCT_DETAILMap());
            modelBuilder.Configurations.Add(new PROVINCEMap());
            modelBuilder.Configurations.Add(new SERVICEMap());
            modelBuilder.Configurations.Add(new SERVICE_DETAILMap());
            modelBuilder.Configurations.Add(new SITEMap());
            modelBuilder.Configurations.Add(new SITE_TYPEMap());
            modelBuilder.Configurations.Add(new STATEMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new THEMEMap());
            modelBuilder.Configurations.Add(new THEME_DETAILMap());
            modelBuilder.Configurations.Add(new TITLEMap());
            modelBuilder.Configurations.Add(new TYPEMap());
            modelBuilder.Configurations.Add(new USER_DETAILMap());
            modelBuilder.Configurations.Add(new USER_TYPEMap());
        }
    }
}
