using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SITEMap : EntityTypeConfiguration<SITE>
    {
        public SITEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_SITE);

            // Properties
            this.Property(t => t.ID_SITE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SITE");
            this.Property(t => t.ID_SITE).HasColumnName("ID_SITE");
            this.Property(t => t.ID_SITETYPE).HasColumnName("ID_SITETYPE");
            this.Property(t => t.ID_USER).HasColumnName("ID_USER");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.SITE_TYPE)
                .WithMany(t => t.SITEs)
                .HasForeignKey(d => d.ID_SITETYPE);

        }
    }
}
