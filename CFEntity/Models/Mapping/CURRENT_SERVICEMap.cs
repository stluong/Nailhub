using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class CURRENT_SERVICEMap : EntityTypeConfiguration<CURRENT_SERVICE>
    {
        public CURRENT_SERVICEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_CURRENT_SERVICE);

            // Properties
            this.Property(t => t.ID_CURRENT_SERVICE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CURRENT_SERVICE");
            this.Property(t => t.ID_CURRENT_SERVICE).HasColumnName("ID_CURRENT_SERVICE");
            this.Property(t => t.ID_SERVICE).HasColumnName("ID_SERVICE");
            this.Property(t => t.ID_SITE).HasColumnName("ID_SITE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.PRICE).HasColumnName("PRICE");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
            this.Property(t => t.ENTERBY).HasColumnName("ENTERBY");
            this.Property(t => t.ENTERDATE).HasColumnName("ENTERDATE");
            this.Property(t => t.MODIFYBY).HasColumnName("MODIFYBY");
            this.Property(t => t.MODIFYDATE).HasColumnName("MODIFYDATE");
            this.Property(t => t.DELETEBY).HasColumnName("DELETEBY");
            this.Property(t => t.DELETEDATE).HasColumnName("DELETEDATE");

            // Relationships
            this.HasOptional(t => t.SERVICE)
                .WithMany(t => t.CURRENT_SERVICE)
                .HasForeignKey(d => d.ID_SERVICE);
            this.HasOptional(t => t.SITE)
                .WithMany(t => t.CURRENT_SERVICE)
                .HasForeignKey(d => d.ID_SITE);

        }
    }
}
