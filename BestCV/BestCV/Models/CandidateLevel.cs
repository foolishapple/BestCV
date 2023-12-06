using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class CandidateLevel:EntityCommon<int>
    {
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giá
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giảm giá
        /// </summary>
        public int DiscountPrice { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Thời gian giảm giá
        /// </summary>
        public DateTime? DiscountEndDate { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Thời gian hết hạn 
        /// </summary>
        public int ExpiryTime { get; set; }

        public ICollection<Candidate> Candidates { get; } = new List<Candidate>();  
        public ICollection<CandidateLevelCandidateLevelBenefit> CandidateLevelCandidateLevelBenefits { get; } = new List<CandidateLevelCandidateLevelBenefit>();
        public ICollection<CandidateOrderDetail> CandidateOrderDetails { get;} = new List<CandidateOrderDetail>();
    }
}
