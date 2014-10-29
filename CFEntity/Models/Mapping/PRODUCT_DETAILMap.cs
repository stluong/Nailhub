using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class PRODUCT_DETAILMap : EntityTypeConfiguration<PRODUCT_DETAIL>
    {
        public PRODUCT_DETAILMap()
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

            this.Property(t => t.IMAGE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PRODUCT_DETAIL");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.PRODUCT_ID).HasColumnName("PRODUCT_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.IMAGE).HasColumnName("IMAGE");

            // Relationships
            this.HasOptional(t => t.PRODUCT)
                .WithMany(t => t.PRODUCT_DETAIL)
                .HasForeignKey(d => d.PRODUCT_ID);

        }
    }
}
