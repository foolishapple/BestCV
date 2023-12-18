using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateLevelCandidateLevelBenefit:EntityCommon<int>
    {
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã cấp ứng viên
        /// </summary>
        public int CandidateLevelId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã cấp lợi ích ứng viên
        /// </summary>
        public int CandidateLevelBenefitId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giá trị
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Quyền lợi được hưởng
        /// </summary>
        public bool HasBenefit { get; set; }

        public virtual CandidateLevel CandidateLevels { get; }
        public virtual CandidateLevelBenefit CandidateLevelBenefits { get; }
    }
}
