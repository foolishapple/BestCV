using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Permissions
{
    public class UpdatePermissionDTO : InsertPermissionDTO
    {
        /// <summary>
        /// Mã vai trò
        /// </summary>
        public int Id { get; set; }
    }
}
