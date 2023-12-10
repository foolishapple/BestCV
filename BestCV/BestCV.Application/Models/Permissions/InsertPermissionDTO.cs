using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Permissions
{
    public class InsertPermissionDTO
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã CODE
        /// </summary>
        public string Code { get; set; } = null!;
        /// <summary>
        /// Danh sách mã vai trò
        /// </summary>
        public HashSet<int> Roles { get; set; } = new();
    }
}
