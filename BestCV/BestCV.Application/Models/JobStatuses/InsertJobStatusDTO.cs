using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobStatuses
{
    /// <summary>
    /// Author: HuyDQ
    /// Created: 27/07/2023
    /// </summary>
    public class InsertJobStatusDTO
    {
        /// <summary>
        /// Tên trạng thái 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Màu trạng thái
        /// </summary>
        public string Color { get; set; }
    }
}
