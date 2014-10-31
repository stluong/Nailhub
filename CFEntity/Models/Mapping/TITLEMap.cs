using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class TITLEMap : EntityTypeConfiguration<TITLE>
    {
        public TITLEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_TITLE);

            // Properties
            this.Property(t => t.ID_TITLE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DESCRIPTION)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NOTE)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("TITLE");
            this.Property(t => t.ID_TITLE).HasColumnName("ID_TITLE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
