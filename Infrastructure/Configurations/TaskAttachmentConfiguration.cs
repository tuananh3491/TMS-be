using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
    {
        public void Configure(EntityTypeBuilder<TaskAttachment> builder)
        {
            builder.ToTable("TaskAttachments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.FileUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(a => a.Task)
                   .WithMany(t => t.Attachments)
                   .HasForeignKey(a => a.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.TaskAttachments)
                   .HasForeignKey(a => a.UploadedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
