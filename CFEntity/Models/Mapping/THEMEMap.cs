using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class THEMEMap : EntityTypeConfiguration<THEME>
    {
        public THEMEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_THEME);

            // Properties
            this.Property(t => t.ID_THEME)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
            this.Property(t => t.ID_THEME).HasColumnName("ID_THEME");
            this.Property(t => t.ID_SITE).HasColumnName("ID_SITE");
            this.Property(t => t.BACKGROUND).HasColumnName("BACKGROUND");
            this.Property(t => t.BACKGROUNDIMAGE).HasColumnName("BACKGROUNDIMAGE");
            this.Property(t => t.LOGO).HasColumnName("LOGO");
            this.Property(t => t.SITENAME).HasColumnName("SITENAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.SITE)
                .WithMany(t => t.THEMEs)
                .HasForeignKey(d => d.ID_SITE);

        }
    }
}
