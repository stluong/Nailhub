using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SITE_TYPEMap : EntityTypeConfiguration<SITE_TYPE>
    {
        public SITE_TYPEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SITE_TYPE");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
