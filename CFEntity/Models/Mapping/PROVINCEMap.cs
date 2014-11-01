using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class PROVINCEMap : EntityTypeConfiguration<PROVINCE>
    {
        public PROVINCEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_PROVINCE);

            // Properties
            this.Property(t => t.ID_PROVINCE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROVINCE");
            this.Property(t => t.ID_PROVINCE).HasColumnName("ID_PROVINCE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
