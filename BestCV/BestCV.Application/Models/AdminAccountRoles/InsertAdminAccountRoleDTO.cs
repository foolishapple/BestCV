using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccountRoles
{
    public class InsertAdminAccountRoleDTO
    {
        /// <summary>
        /// Mã tài khoản quản trị viên
        /// </summary>
        public long AdminAccountId { get; set; }
        /// <summary>
        /// Mã vai trò
        /// </summary>
        public int RoleId { get; set; }
    }
}
