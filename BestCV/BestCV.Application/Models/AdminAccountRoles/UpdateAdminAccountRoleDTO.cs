using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccountRoles
{
    public class UpdateAdminAccountRoleDTO : InsertAdminAccountRoleDTO
    {
        /// <summary>
        /// Mã vai trò tài khoản quản trị viên
        /// </summary>
        public int Id { get; set; }
    }
}
