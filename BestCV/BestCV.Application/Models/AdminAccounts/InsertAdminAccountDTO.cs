using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccounts
{

    public class InsertAdminAccountDTO
    {
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; } = null!;
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        /// 
        public string Phone { get; set; } = null!;
        public string? Photo { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = null!;
        public bool LockEnabled { get; set; }
        public List<int> Roles { get; set; } = new();
    }
}
