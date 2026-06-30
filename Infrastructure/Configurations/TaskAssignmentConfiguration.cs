using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
    {
        public void Configure(EntityTypeBuilder<TaskAssignment> builder)
        {
            builder.ToTable("TaskAssignments");

            builder.HasKey(ta => new { ta.TaskId, ta.UserId });

            builder.HasOne(ta => ta.Task)
                   .WithMany(t => t.Assignments)
                   .HasForeignKey(ta => ta.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ta => ta.User)
                   .WithMany(u => u.TaskAssignments)
                   .HasForeignKey(ta => ta.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
