using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public String? Description { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public User Owner { get; set; } = null!;

        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}