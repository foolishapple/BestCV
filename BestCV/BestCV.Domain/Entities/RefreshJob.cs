using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class RefreshJob : EntityBase<long>
    {
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// Ngày làm mới
        /// </summary>
        public DateTime RefreshDate { get; set; }
        public virtual Job Job { get; } = null!;
    }
}
