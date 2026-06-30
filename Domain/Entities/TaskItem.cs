using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = String.Empty;
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public string Status { get; set; } = String.Empty;
        public DateTime DueDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Project Project { get; set; } = null!;
        public User Creator { get; set; } = null!;
        
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();
    }
}