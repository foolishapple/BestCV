using BestCV.Core.Entities.Interfaces;
using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Việc làm gấp
    /// </summary>
    public class TopJobUrgent : EntityBase<long>, IEntityBase<long>
    {
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public virtual Job Job { get; } = null!;
    }
}
