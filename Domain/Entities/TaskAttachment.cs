using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskAttachment
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string FileName { get; set; } = String.Empty;
        public string FileUrl { get; set; } = String.Empty;
        public Guid UploadedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TaskItem Task { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}