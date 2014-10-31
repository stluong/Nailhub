using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class SERVICE_DETAILMap : EntityTypeConfiguration<SERVICE_DETAIL>
    {
        public SERVICE_DETAILMap()
        {
            // Primary Key
            this.HasKey(t => t.ID_SERVICEDETAIL);

            // Properties
            this.Property(t => t.ID_SERVICEDETAIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SERVICE_DETAIL");
            this.Property(t => t.ID_SERVICEDETAIL).HasColumnName("ID_SERVICEDETAIL");
            this.Property(t => t.ID_SERVICE).HasColumnName("ID_SERVICE");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.NOTE).HasColumnName("NOTE");

            // Relationships
            this.HasOptional(t => t.SERVICE)
                .WithMany(t => t.SERVICE_DETAIL)
                .HasForeignKey(d => d.ID_SERVICE);

        }
    }
}
