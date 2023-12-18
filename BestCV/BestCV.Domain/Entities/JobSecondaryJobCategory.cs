using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Ngành nghề liên quan phụ
    /// </summary>
    public class JobSecondaryJobCategory : EntityBase<long>
    {
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }

        /// <summary>
        /// Mã vị trí tuyển dụng
        /// </summary>
        public int JobCategoryId { get; set; }

        public virtual Job Job { get; } = null!;
        public virtual JobCategory JobCategory { get; } = null!;
    }
}
