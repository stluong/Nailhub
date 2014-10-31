using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class EMPLOYEEMap : EntityTypeConfiguration<EMPLOYEE>
    {
        public EMPLOYEEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_EMPLYEE);

            // Properties
            this.Property(t => t.ID_EMPLYEE)
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
            this.Property(t => t.ID_EMPLYEE).HasColumnName("ID_EMPLYEE");
            this.Property(t => t.ID_TITLE).HasColumnName("ID_TITLE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");

            // Relationships
            this.HasOptional(t => t.TITLE)
                .WithMany(t => t.EMPLOYEEs)
                .HasForeignKey(d => d.ID_TITLE);

        }
    }
}
