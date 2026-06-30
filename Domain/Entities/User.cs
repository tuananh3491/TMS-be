using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Status { get; set; } = true;
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; } = null!;

        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
        public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
        public ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();
    }
}