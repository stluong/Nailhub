using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class THEME_DETAILMap : EntityTypeConfiguration<THEME_DETAIL>
    {
        public THEME_DETAILMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_THEMEDETAIL);

            // Properties
            this.Property(t => t.ID_THEMEDETAIL)
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
            this.Property(t => t.ID_THEMEDETAIL).HasColumnName("ID_THEMEDETAIL");
            this.Property(t => t.ID_THEME).HasColumnName("ID_THEME");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPT).HasColumnName("DESCRIPT");
            this.Property(t => t.IMAGE).HasColumnName("IMAGE");
            this.Property(t => t.STYLE).HasColumnName("STYLE");

            // Relationships
            this.HasOptional(t => t.THEME)
                .WithMany(t => t.THEME_DETAIL)
                .HasForeignKey(d => d.ID_THEME);

        }
    }
}
