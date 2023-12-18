using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class Menu:EntityCommon<int>
    {
        public int? ParentId { get; set; }
        public int MenuTypeId { get; set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public string TreeIds { get; set; } = null!;
        public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
        public virtual MenuType MenuType { get; } = null!;
    }
}
