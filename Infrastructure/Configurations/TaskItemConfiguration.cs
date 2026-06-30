using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Priority)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(t => t.DueDate)
                   .IsRequired();

            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Assignments)
                   .WithOne(ta => ta.Task)
                   .HasForeignKey(ta => ta.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Comments)
                   .WithOne(c => c.Task)
                   .HasForeignKey(c => c.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Attachments)
                   .WithOne(a => a.Task)
                   .HasForeignKey(a => a.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Creator)
                   .WithMany(u => u.CreatedTasks)
                   .HasForeignKey(t => t.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
