using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RolePermissions
{
    public class UpdateRolePermissionDTO : InsertRolePermissionDTO
    {
        /// <summary>
        /// Mã quyền vai  trò
        /// </summary>
        public int Id { get; set; }
    }
}
