using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class EMPLOYEEMap : EntityTypeConfiguration<EMPLOYEE>
    {
        public EMPLOYEEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NAME)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DESCRIPTION)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("EMPLOYEE");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.TITLE_ID).HasColumnName("TITLE_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");

            // Relationships
            this.HasOptional(t => t.TITLE)
                .WithMany(t => t.EMPLOYEEs)
                .HasForeignKey(d => d.TITLE_ID);

        }
    }
}
