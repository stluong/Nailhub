using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class COUNTRYMap : EntityTypeConfiguration<COUNTRY>
    {
        public COUNTRYMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_COUNTRY);

            // Properties
            this.Property(t => t.ID_COUNTRY)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.NATIVELANGUAGE)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("COUNTRY");
            this.Property(t => t.ID_COUNTRY).HasColumnName("ID_COUNTRY");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.NATIVELANGUAGE).HasColumnName("NATIVELANGUAGE");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
