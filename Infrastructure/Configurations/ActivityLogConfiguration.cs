using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");

            builder.HasKey(al => al.Id);

            builder.Property(al => al.Action)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(al => al.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(al => al.User)
                   .WithMany(u => u.ActivityLogs)
                   .HasForeignKey(al => al.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
