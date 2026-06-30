using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {
            builder.ToTable("ProjectMembers");

            builder.HasKey(pm => new { pm.ProjectId, pm.UserId });

            builder.Property(pm => pm.RoleInProject)
                   .HasMaxLength(50);

            builder.HasOne(pm => pm.Project)
                   .WithMany(p => p.Members)
                   .HasForeignKey(pm => pm.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pm => pm.User)
                   .WithMany(u => u.ProjectMembers)
                   .HasForeignKey(pm => pm.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
