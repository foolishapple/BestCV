using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class Role:EntityCommon<int>
    {
        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; } = null!;
        public virtual ICollection<AdminAccountRole> AdminAccountRoles { get;} = new List<AdminAccountRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();
        public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
    }
}
