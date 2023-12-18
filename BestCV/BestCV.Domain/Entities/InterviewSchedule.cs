using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class InterviewSchedule : EntityCommon<int>, IFullTextSearch
    {
        /// <summary>
        /// Loại lịch phỏng vấn
        /// </summary>
        public int InterviewscheduleTypeId { get; set; }
        /// <summary>
        /// Trạng thái lịch phỏng vấn
        /// </summary>
        public int InterviewscheduleStatusId { get; set; }
        /// <summary>
        /// Mã ứng viên nôp hồ sơ việc làm
        /// </summary>
        public long CandidateApplyJobId { get; set; }
        /// <summary>
        /// Đường link
        /// </summary>
        public string Link { get; set; }
        public DateTime StateDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Vị trí
        /// </summary>
        public string Location { get; set; }
        public string Search { get; set; }

        public virtual InterviewType InterviewType { get; } 
        public virtual InterviewStatus InterviewStatus { get;} 
        public virtual CandidateApplyJob CandidateApplyJob { get; }
    }
}
