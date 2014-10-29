using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class CURRENT_PRODUCTMap : EntityTypeConfiguration<CURRENT_PRODUCT>
    {
        public CURRENT_PRODUCTMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.PRICE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CURRENT_PRODUCT");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.PRODUCT_ID).HasColumnName("PRODUCT_ID");
            this.Property(t => t.SITE_ID).HasColumnName("SITE_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.PRICE).HasColumnName("PRICE");
            this.Property(t => t.ENTERBY).HasColumnName("ENTERBY");
            this.Property(t => t.ENTERDATE).HasColumnName("ENTERDATE");
            this.Property(t => t.MODIFYBY).HasColumnName("MODIFYBY");
            this.Property(t => t.MODIFYDATE).HasColumnName("MODIFYDATE");
            this.Property(t => t.DELETEBY).HasColumnName("DELETEBY");
            this.Property(t => t.DELETEDATE).HasColumnName("DELETEDATE");

            // Relationships
            this.HasOptional(t => t.PRODUCT)
                .WithMany(t => t.CURRENT_PRODUCT)
                .HasForeignKey(d => d.PRODUCT_ID);
            this.HasOptional(t => t.SITE)
                .WithMany(t => t.CURRENT_PRODUCT)
                .HasForeignKey(d => d.SITE_ID);

        }
    }
}
