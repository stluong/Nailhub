using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CFEntity.Models.Mapping
{
    public class USER_DETAILMap : EntityTypeConfiguration<USER_DETAIL>
    {
        public USER_DETAILMap()
        {
            // Primary Key
            this.HasKey(t => new { t.userId, t.UserTypeId });

            // Properties
            this.Property(t => t.userId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FIRSTNAME)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LASTNAME)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("USER_DETAIL");
            this.Property(t => t.userId).HasColumnName("userId");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.FIRSTNAME).HasColumnName("FIRSTNAME");
            this.Property(t => t.LASTNAME).HasColumnName("LASTNAME");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.USER_DETAIL)
                .HasForeignKey(d => d.userId);
            this.HasRequired(t => t.USER_TYPE)
                .WithMany(t => t.USER_DETAIL)
                .HasForeignKey(d => d.UserTypeId);

        }
    }
}
