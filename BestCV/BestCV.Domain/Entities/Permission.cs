using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class Permission : EntityCommon<int>
    {
        /// <summary>
        /// Mã Code
        /// </summary>
        public string Code { get; set; } = null!;
        public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();
    }
}
