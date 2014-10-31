using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class PRODUCTMap : EntityTypeConfiguration<PRODUCT>
    {
        public PRODUCTMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_PRODUCT);

            // Properties
            this.Property(t => t.ID_PRODUCT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PRODUCT");
            this.Property(t => t.ID_PRODUCT).HasColumnName("ID_PRODUCT");
            this.Property(t => t.ID_TYPE).HasColumnName("ID_TYPE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.TYPE)
                .WithMany(t => t.PRODUCTs)
                .HasForeignKey(d => d.ID_TYPE);

        }
    }
}
