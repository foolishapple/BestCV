using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateEducation : EntityBase<long>
    {
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get;set; }
        /// <summary>
        /// Trường học
        /// </summary>
        public string School { get;set; }
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get;set; }
        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public string TimePeriod { get;set; }
        public string? Description { get;set; }

        public virtual Candidate Candidate { get; }
    }
}
