using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobServicePackages
{
    public class InsertJobServicePackageDTO
    {
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        public int? ExpireTime { get; set; }
        public int? Value { get; set; }
        public int Quantity { get; set; }
        public int EmployerServicePackageId { get; set; }
        /// <summary>
        /// Mã nhà tuyển dụng
        /// </summary>
        public long? EmployerId { get; set; }
        public long OrderId { get; set; }
    }
}
