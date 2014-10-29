using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class THEME_DETAILMap : EntityTypeConfiguration<THEME_DETAIL>
    {
        public THEME_DETAILMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPT)
                .HasMaxLength(50);

            this.Property(t => t.IMAGE)
                .HasMaxLength(50);

            this.Property(t => t.STYLE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("THEME_DETAIL");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.THEME_ID).HasColumnName("THEME_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPT).HasColumnName("DESCRIPT");
            this.Property(t => t.IMAGE).HasColumnName("IMAGE");
            this.Property(t => t.STYLE).HasColumnName("STYLE");

            // Relationships
            this.HasOptional(t => t.THEME)
                .WithMany(t => t.THEME_DETAIL)
                .HasForeignKey(d => d.THEME_ID);

        }
    }
}
