using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class USER_TYPEMap : EntityTypeConfiguration<USER_TYPE>
    {
        public USER_TYPEMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.NAME)
                .HasMaxLength(50);

            this.Property(t => t.NOTE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("USER_TYPE");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.NAME).HasColumnName("NAME");
            this.Property(t => t.NOTE).HasColumnName("NOTE");
        }
    }
}
