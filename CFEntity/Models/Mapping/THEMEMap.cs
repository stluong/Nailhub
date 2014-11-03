using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class THEMEMap : EntityTypeConfiguration<THEME>
    {
        public THEMEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.BACKGROUND)
                .HasMaxLength(50);

            this.Property(t => t.BACKGROUNDIMAGE)
                .HasMaxLength(50);

            this.Property(t => t.LOGO)
                .HasMaxLength(50);

            this.Property(t => t.SITENAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("THEME");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.SITE_ID).HasColumnName("SITE_ID");
            this.Property(t => t.BACKGROUND).HasColumnName("BACKGROUND");
            this.Property(t => t.BACKGROUNDIMAGE).HasColumnName("BACKGROUNDIMAGE");
            this.Property(t => t.LOGO).HasColumnName("LOGO");
            this.Property(t => t.SITENAME).HasColumnName("SITENAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.SITE)
                .WithMany(t => t.THEMEs)
                .HasForeignKey(d => d.SITE_ID);

        }
    }
}
