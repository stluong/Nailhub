using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SERVICEMap : EntityTypeConfiguration<SERVICE>
    {
        public SERVICEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SERVICE");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.TYPE_ID).HasColumnName("TYPE_ID");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.TYPE)
                .WithMany(t => t.SERVICEs)
                .HasForeignKey(d => d.TYPE_ID);

        }
    }
}
