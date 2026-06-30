using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        // Fixed GUIDs for seed data consistency
        private static readonly Guid RoleAdminId = Guid.Parse("A1B2C3D4-E5F6-7890-ABCD-EF1234567890");
        private static readonly Guid RoleManagerId = Guid.Parse("B2C3D4E5-F6A7-8901-BCDE-F12345678901");
        private static readonly Guid RoleDeveloperId = Guid.Parse("C3D4E5F6-A7B8-9012-CDEF-123456789012");

        private static readonly Guid AdminUserId = Guid.Parse("D4E5F6A7-B8C9-0123-DEF0-1234567890AB");
        private static readonly Guid ManagerUserId = Guid.Parse("E5F6A7B8-C9D0-1234-EF01-234567890ABC");
        private static readonly Guid DeveloperUserId = Guid.Parse("F6A7B8C9-D0E1-2345-F012-34567890ABCD");

        private static readonly Guid ProjectAlphaId = Guid.Parse("A1A2A3A4-B1B2-C1C2-D1D2-E1E2E3E4E5E6");
        private static readonly Guid ProjectBetaId = Guid.Parse("B1B2B3B4-C1C2-D1D2-E1E2-F1F2F3F4F5F6");

        private static readonly Guid TaskAlpha1Id = Guid.Parse("C1C2C3C4-D1D2-E1E2-F1F2-A1A2A3A4A5A6");
        private static readonly Guid TaskAlpha2Id = Guid.Parse("D1D2D3D4-E1E2-F1F2-A1A2-B1B2B3B4B5B6");
        private static readonly Guid TaskBeta1Id = Guid.Parse("E1E2E3E4-F1F2-A1A2-B1B2-C1C2C3C4C5C6");
        private static readonly Guid TaskBeta2Id = Guid.Parse("F1F2F3F4-A1A2-B1B2-C1C2-D1D2D3D4D5D6");

        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            await SeedRolesAsync(context);
            await SeedUsersAsync(context);
            await SeedProjectsAsync(context);
            await SeedProjectMembersAsync(context);
            await SeedTaskItemsAsync(context);
            await SeedTaskAssignmentsAsync(context);
            await SeedCommentsAsync(context);
            await SeedTaskAttachmentsAsync(context);
            await SeedNotificationsAsync(context);
            await SeedActivityLogsAsync(context);
            await SeedRefreshTokensAsync(context);
        }

        private static async Task SeedRolesAsync(AppDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var roles = new List<Role>
            {
                new()
                {
                    Id = RoleAdminId,
                    Name = RoleName.Admin.ToString(),
                    Description = "System administrator with full access"
                },
                new()
                {
                    Id = RoleManagerId,
                    Name = RoleName.Manager.ToString(),
                    Description = "Project manager, manages teams and resources"
                },
                new()
                {
                    Id = RoleDeveloperId,
                    Name = RoleName.Developer.ToString(),
                    Description = "Developer, performs technical tasks"
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            // In production, use proper password hashing. For seed data, using a placeholder hash.
            const string placeholderHash = "$2a$11$SeedDataPlaceholderHashNotForProduction";

            var users = new List<User>
            {
                new()
                {
                    Id = AdminUserId,
                    FullName = "Alice Admin",
                    Email = "admin@taskman.com",
                    PasswordHash = placeholderHash,
                    PhoneNumber = "0901-234-567",
                    DateOfBirth = new DateTime(1990, 1, 15),
                    Status = true,
                    RoleId = RoleAdminId,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = ManagerUserId,
                    FullName = "Charlie Manager",
                    Email = "manager@taskman.com",
                    PasswordHash = placeholderHash,
                    PhoneNumber = "0902-345-678",
                    DateOfBirth = new DateTime(1992, 5, 20),
                    Status = true,
                    RoleId = RoleManagerId,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = DeveloperUserId,
                    FullName = "Bob Developer",
                    Email = "developer@taskman.com",
                    PasswordHash = placeholderHash,
                    PhoneNumber = "0903-456-789",
                    DateOfBirth = new DateTime(1995, 8, 10),
                    Status = true,
                    RoleId = RoleDeveloperId,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        private static async Task SeedProjectsAsync(AppDbContext context)
        {
            if (await context.Projects.AnyAsync())
                return;

            var projects = new List<Project>
            {
                new()
                {
                    Id = ProjectAlphaId,
                    Name = "Task Management System",
                    Description = "Build an internal task management system for the company",
                    OwnerId = ManagerUserId,
                    StartDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                    Status = "Active",
                    CreatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = ProjectBetaId,
                    Name = "E-Commerce Website",
                    Description = "Develop an online shopping website",
                    OwnerId = ManagerUserId,
                    StartDate = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                    Status = "Planning",
                    CreatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();
        }

        private static async Task SeedProjectMembersAsync(AppDbContext context)
        {
            if (await context.ProjectMembers.AnyAsync())
                return;

            var members = new List<ProjectMember>
            {
                new()
                {
                    ProjectId = ProjectAlphaId,
                    UserId = ManagerUserId,
                    RoleInProject = "Project Manager",
                    JoinedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    ProjectId = ProjectAlphaId,
                    UserId = DeveloperUserId,
                    RoleInProject = "Full-stack Developer",
                    JoinedAt = new DateTime(2025, 6, 5, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    ProjectId = ProjectBetaId,
                    UserId = ManagerUserId,
                    RoleInProject = "Project Manager",
                    JoinedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    ProjectId = ProjectBetaId,
                    UserId = DeveloperUserId,
                    RoleInProject = "Backend Developer",
                    JoinedAt = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.ProjectMembers.AddRangeAsync(members);
            await context.SaveChangesAsync();
        }

        private static async Task SeedTaskItemsAsync(AppDbContext context)
        {
            if (await context.TaskItems.AnyAsync())
                return;

            var tasks = new List<TaskItem>
            {
                new()
                {
                    Id = TaskAlpha1Id,
                    ProjectId = ProjectAlphaId,
                    Title = "Database Design",
                    Description = "Design ERD and create migrations for the entire system",
                    Priority = Priority.High,
                    Status = "Done",
                    DueDate = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 14, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = TaskAlpha2Id,
                    ProjectId = ProjectAlphaId,
                    Title = "Develop Authentication API",
                    Description = "Build login, registration and JWT token management module",
                    Priority = Priority.Urgent,
                    Status = "InProgress",
                    DueDate = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 20, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = TaskBeta1Id,
                    ProjectId = ProjectBetaId,
                    Title = "Build Shopping Cart",
                    Description = "Develop add/edit/delete product functionality in the shopping cart",
                    Priority = Priority.High,
                    Status = "Todo",
                    DueDate = new DateTime(2025, 8, 15, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = TaskBeta2Id,
                    ProjectId = ProjectBetaId,
                    Title = "Integrate Payment Gateway",
                    Description = "Integrate VNPay and MoMo for the e-commerce website",
                    Priority = Priority.Medium,
                    Status = "Review",
                    DueDate = new DateTime(2025, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 7, 5, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 7, 20, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.TaskItems.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }

        private static async Task SeedTaskAssignmentsAsync(AppDbContext context)
        {
            if (await context.TaskAssignments.AnyAsync())
                return;

            var assignments = new List<TaskAssignment>
            {
                new()
                {
                    TaskId = TaskAlpha1Id,
                    UserId = DeveloperUserId,
                    AssignedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    TaskId = TaskAlpha2Id,
                    UserId = DeveloperUserId,
                    AssignedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    TaskId = TaskBeta1Id,
                    UserId = DeveloperUserId,
                    AssignedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    TaskId = TaskBeta2Id,
                    UserId = DeveloperUserId,
                    AssignedAt = new DateTime(2025, 7, 5, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.TaskAssignments.AddRangeAsync(assignments);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCommentsAsync(AppDbContext context)
        {
            if (await context.Comments.AnyAsync())
                return;

            var comments = new List<Comment>
            {
                new()
                {
                    Id = Guid.Parse("A1B1C1D1-E1F1-1111-1111-111111111111"),
                    TaskId = TaskAlpha1Id,
                    UserId = DeveloperUserId,
                    Content = "Completed database design. Pushed to Git for review.",
                    CreatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("A2B2C2D2-E2F2-2222-2222-222222222222"),
                    TaskId = TaskAlpha1Id,
                    UserId = ManagerUserId,
                    Content = "Database structure looks good. Consider adding indexes for frequently queried columns.",
                    CreatedAt = new DateTime(2025, 6, 11, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 11, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("A3B3C3D3-E3F3-3333-3333-333333333333"),
                    TaskId = TaskAlpha2Id,
                    UserId = DeveloperUserId,
                    Content = "Having issues with refresh token. Need more time to fix.",
                    CreatedAt = new DateTime(2025, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 6, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("A4B4C4D4-E4F4-4444-4444-444444444444"),
                    TaskId = TaskBeta2Id,
                    UserId = DeveloperUserId,
                    Content = "Reviewed VNPay API documentation. Will start integration next week.",
                    CreatedAt = new DateTime(2025, 7, 22, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 7, 22, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.Comments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }

        private static async Task SeedTaskAttachmentsAsync(AppDbContext context)
        {
            if (await context.TaskAttachments.AnyAsync())
                return;

            var attachments = new List<TaskAttachment>
            {
                new()
                {
                    Id = Guid.Parse("B1C1D1E1-F1A1-1111-1111-111111111111"),
                    TaskId = TaskAlpha1Id,
                    FileName = "ERD-Diagram.pdf",
                    FileUrl = "https://storage.taskman.com/seeds/erd-diagram.pdf",
                    UploadedBy = DeveloperUserId,
                    CreatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("B2C2D2E2-F2A2-2222-2222-222222222222"),
                    TaskId = TaskAlpha2Id,
                    FileName = "Auth-API-Spec.docx",
                    FileUrl = "https://storage.taskman.com/seeds/auth-api-spec.docx",
                    UploadedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 6, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("B3C3D3E3-F3A3-3333-3333-333333333333"),
                    TaskId = TaskBeta2Id,
                    FileName = "VNPay-Integration-Guide.pdf",
                    FileUrl = "https://storage.taskman.com/seeds/vnpay-guide.pdf",
                    UploadedBy = ManagerUserId,
                    CreatedAt = new DateTime(2025, 7, 15, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.TaskAttachments.AddRangeAsync(attachments);
            await context.SaveChangesAsync();
        }

        private static async Task SeedNotificationsAsync(AppDbContext context)
        {
            if (await context.Notifications.AnyAsync())
                return;

            var notifications = new List<Notification>
            {
                new()
                {
                    Id = Guid.Parse("C1D1E1F1-A1B1-1111-1111-111111111111"),
                    UserId = DeveloperUserId,
                    Title = "New Task Assigned",
                    Message = "You have been assigned to task 'Develop Authentication API' in project 'Task Management System'.",
                    IsRead = true,
                    CreatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("C2D2E2F2-A2B2-2222-2222-222222222222"),
                    UserId = DeveloperUserId,
                    Title = "New Task Assigned",
                    Message = "You have been assigned to task 'Build Shopping Cart' in project 'E-Commerce Website'.",
                    IsRead = false,
                    CreatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("C3D3E3F3-A3B3-3333-3333-333333333333"),
                    UserId = ManagerUserId,
                    Title = "New Comment",
                    Message = "Bob Developer commented on task 'Database Design'.",
                    IsRead = true,
                    CreatedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("C4D4E4F4-A4B4-4444-4444-444444444444"),
                    UserId = ManagerUserId,
                    Title = "Support Request",
                    Message = "Bob Developer is having issues with task 'Develop Authentication API'. Please review.",
                    IsRead = false,
                    CreatedAt = new DateTime(2025, 6, 25, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.Notifications.AddRangeAsync(notifications);
            await context.SaveChangesAsync();
        }

        private static async Task SeedActivityLogsAsync(AppDbContext context)
        {
            if (await context.ActivityLogs.AnyAsync())
                return;

            var logs = new List<ActivityLog>
            {
                new()
                {
                    Id = Guid.Parse("D1E1F1A1-B1C1-1111-1111-111111111111"),
                    UserId = ManagerUserId,
                    Action = "Created project",
                    EntityName = "Project",
                    EntityId = ProjectAlphaId,
                    CreatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("D2E2F2A2-B2C2-2222-2222-222222222222"),
                    UserId = DeveloperUserId,
                    Action = "Completed task",
                    EntityName = "TaskItem",
                    EntityId = TaskAlpha1Id,
                    CreatedAt = new DateTime(2025, 6, 14, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("D3E3F3A3-B3C3-3333-3333-333333333333"),
                    UserId = ManagerUserId,
                    Action = "Created project",
                    EntityName = "Project",
                    EntityId = ProjectBetaId,
                    CreatedAt = new DateTime(2025, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("D4E4F4A4-B4C4-4444-4444-444444444444"),
                    UserId = ManagerUserId,
                    Action = "Added member",
                    EntityName = "ProjectMember",
                    EntityId = Guid.Empty,
                    CreatedAt = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.ActivityLogs.AddRangeAsync(logs);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRefreshTokensAsync(AppDbContext context)
        {
            if (await context.RefreshTokens.AnyAsync())
                return;

            var refreshTokens = new List<RefreshToken>
            {
                new()
                {
                    Id = Guid.Parse("E1F1A1B1-C1D1-1111-1111-111111111111"),
                    UserId = AdminUserId,
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                    ExpiresDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsRevoked = false,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("E2F2A2B2-C2D2-2222-2222-222222222222"),
                    UserId = ManagerUserId,
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                    ExpiresDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsRevoked = false,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Id = Guid.Parse("E3F3A3B3-C3D3-3333-3333-333333333333"),
                    UserId = DeveloperUserId,
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                    ExpiresDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsRevoked = false,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.RefreshTokens.AddRangeAsync(refreshTokens);
            await context.SaveChangesAsync();
        }
    }
}
