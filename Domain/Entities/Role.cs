using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}