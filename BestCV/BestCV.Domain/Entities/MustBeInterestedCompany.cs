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
    /// Danh sách NTD có thể bạn quan tâm
    /// </summary>
    public class MustBeInterestedCompany : EntityBase<long>, IEntityBase<long>
    {
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public int CompanyId { get; set; }
        public virtual Company Company { get; } = null!;
    }
}
