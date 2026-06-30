using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.StartDate)
                   .IsRequired();

            builder.Property(p => p.EndDate)
                   .IsRequired();

            builder.HasOne(p => p.Owner)
                   .WithMany(u => u.OwnedProjects)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Members)
                   .WithOne(pm => pm.Project)
                   .HasForeignKey(pm => pm.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Tasks)
                   .WithOne(t => t.Project)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
