using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.SystemConfigs
{
    public class InsertSystemConfigDTO
    {
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string Key { get; set; } = null!;
        /// <summary>
        /// Giá trị
        /// </summary>
        public string Value { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
