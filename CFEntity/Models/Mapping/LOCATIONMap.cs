using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class LOCATIONMap : EntityTypeConfiguration<LOCATION>
    {
        public LOCATIONMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
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
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.ADDRESS).HasColumnName("ADDRESS");
            this.Property(t => t.ADDRESS2).HasColumnName("ADDRESS2");
            this.Property(t => t.STATE_ID).HasColumnName("STATE_ID");
            this.Property(t => t.CITY_ID).HasColumnName("CITY_ID");
            this.Property(t => t.ZIP).HasColumnName("ZIP");
            this.Property(t => t.COUNTRY_ID).HasColumnName("COUNTRY_ID");

            // Relationships
            this.HasOptional(t => t.CITY)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.CITY_ID);
            this.HasOptional(t => t.COUNTRY)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.COUNTRY_ID);
            this.HasOptional(t => t.STATE)
                .WithMany(t => t.LOCATIONs)
                .HasForeignKey(d => d.STATE_ID);

        }
    }
}
