using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SITEMap : EntityTypeConfiguration<SITE>
    {
        public SITEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SITE");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.SITE_TYPE_ID).HasColumnName("SITE_TYPE_ID");
            this.Property(t => t.USER_ID).HasColumnName("USER_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.SITEs)
                .HasForeignKey(d => d.USER_ID);
            this.HasOptional(t => t.SITE_TYPE)
                .WithMany(t => t.SITEs)
                .HasForeignKey(d => d.SITE_TYPE_ID);

        }
    }
}
