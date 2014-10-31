using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class USER_DETAILMap : EntityTypeConfiguration<USER_DETAIL>
    {
        public USER_DETAILMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID_USER, t.ID_USERTYPE });

            // Properties
            this.Property(t => t.ID_USER)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ID_USERTYPE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FIRSTNAME)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("USER_DETAIL");
            this.Property(t => t.ID_USER).HasColumnName("ID_USER");
            this.Property(t => t.ID_USERTYPE).HasColumnName("ID_USERTYPE");
            this.Property(t => t.FIRSTNAME).HasColumnName("FIRSTNAME");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.USER_DETAIL)
                .HasForeignKey(d => d.ID_USER);
            this.HasRequired(t => t.USER_TYPE)
                .WithMany(t => t.USER_DETAIL)
                .HasForeignKey(d => d.ID_USERTYPE);

        }
    }
}
