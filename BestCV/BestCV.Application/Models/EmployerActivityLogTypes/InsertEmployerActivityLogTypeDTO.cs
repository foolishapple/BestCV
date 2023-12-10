using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerActivityLogTypes
{
    public class InsertEmployerActivityLogTypeDTO
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
