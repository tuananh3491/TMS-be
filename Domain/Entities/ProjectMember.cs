using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectMember
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public string? RoleInProject { get; set; }
        public Project Project { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}