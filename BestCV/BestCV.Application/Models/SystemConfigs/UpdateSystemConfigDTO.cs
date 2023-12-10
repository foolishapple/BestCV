using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.SystemConfigs
{
    public class UpdateSystemConfigDTO : InsertSystemConfigDTO
    {
        /// <summary>
        /// Mã cấu hình hệ thống
        /// </summary>
        public int Id { get; set; }
    }
}
