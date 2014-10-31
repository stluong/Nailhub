using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SERVICEMap : EntityTypeConfiguration<SERVICE>
    {
        public SERVICEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_SERVICE);

            // Properties
            this.Property(t => t.ID_SERVICE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SERVICE");
            this.Property(t => t.ID_SERVICE).HasColumnName("ID_SERVICE");
            this.Property(t => t.ID_TYPE).HasColumnName("ID_TYPE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.TYPE)
                .WithMany(t => t.SERVICEs)
                .HasForeignKey(d => d.ID_TYPE);

        }
    }
}
