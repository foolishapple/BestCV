using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RolePermissions
{
    public class UpdateListRolePermissionDTO
    {
        /// <summary>
        /// List add role permission
        /// </summary>
        public HashSet<InsertRolePermissionDTO> AddItems { get; set; } = new();
        /// <summary>
        /// List delete role permission
        /// </summary>
        public HashSet<int> DeleteItems { get; set; } = new();
    }
}
