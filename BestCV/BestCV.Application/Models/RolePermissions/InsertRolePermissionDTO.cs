using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RolePermissions
{
    public class InsertRolePermissionDTO
    {
        /// <summary>
        /// Mã vai trò
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Mã quyền
        /// </summary>
        public int PermissionId { get; set; }
    }
}
