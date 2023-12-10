using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.AdminAccounts
{
    public class AdminAccountAggregate 
    {
        public long Id { get; set; }

        public string UserName { get; set; } = null!;

        public HashSet<int> Roles { get; set; } = new();

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        public string FullName { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public bool LockEnabled { get; set; }
    }
}
