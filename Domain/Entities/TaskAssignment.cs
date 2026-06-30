using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskAssignment
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public TaskItem Task { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}