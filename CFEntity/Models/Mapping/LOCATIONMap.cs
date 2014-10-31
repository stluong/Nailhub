using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class LOCATIONMap : EntityTypeConfiguration<LOCATION>
    {
        public LOCATIONMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_LOCATION);

            // Properties
            this.Property(t => t.ID_LOCATION)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ADDRESS)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ADDRESS2)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ZIP)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("LOCATION");
            this.Property(t => t.ID_LOCATION).HasColumnName("ID_LOCATION");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.ADDRESS).HasColumnName("ADDRESS");
            this.Property(t => t.ADDRESS2).HasColumnName("ADDRESS2");
            this.Property(t => t.ID_STATE).HasColumnName("ID_STATE");
            this.Property(t => t.ID_CITY).HasColumnName("ID_CITY");
            this.Property(t => t.ZIP).HasColumnName("ZIP");
            this.Property(t => t.ID_COUNTRY).HasColumnName("ID_COUNTRY");

            // Relationships
            this.HasOptional(t => t.CITY)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.ID_CITY);
            this.HasOptional(t => t.COUNTRY)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.ID_COUNTRY);
            this.HasOptional(t => t.STATE)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.ID_STATE);

        }
    }
}
