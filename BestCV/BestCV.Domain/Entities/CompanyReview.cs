using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CompanyReview : EntityBase<long>
    {
        /// <summary>
        /// Mã công ty
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }

        /// <summary>
        /// Nhận xét
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Chấp thuận
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Ngày chấp thuận
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Candidate Candidate { get; set; } = null!;
    }
}
