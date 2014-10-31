using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class CITYMap : EntityTypeConfiguration<CITY>
    {
        public CITYMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_CITY);

            // Properties
            this.Property(t => t.ID_CITY)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CITY");
            this.Property(t => t.ID_CITY).HasColumnName("ID_CITY");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
