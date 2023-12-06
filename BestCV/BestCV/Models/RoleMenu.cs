using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class RoleMenu : EntityBase<int>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
    }
}
