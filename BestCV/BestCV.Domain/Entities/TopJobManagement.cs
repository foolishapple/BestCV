using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    /// <summary>
    /// Việc làm quản lý
    /// </summary>
    public class TopJobManagement : EntityBase<long> ,IEntityBase<long>
    {
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        public virtual Job Job { get; } = null!;
    }
}
