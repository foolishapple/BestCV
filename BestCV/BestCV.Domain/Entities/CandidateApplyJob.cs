using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateApplyJob : EntityBase<long>
    {
        /// <summary>
        /// Mã CV PDF của ứng viên
        /// </summary>
        public long CandidateCVPDFId { get; set; }
        /// <summary>
        /// Mã việc làm
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// Mã trạng thái ứng viên ứng tuyển việc làm
        /// </summary>
        public int CandidateApplyJobStatusId { get; set; }
        /// <summary>
        /// Mã nguồn ứng viên ứng tuyển việc làm
        /// </summary>
        public int CandidateApplyJobSourceId { get; set; }
        /// <summary>
        /// Nhà tuyển dụng đã xem
        /// </summary>
        public bool IsEmployerViewed { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        public virtual CandidateCVPDF CandidateCVPDF { get; } 
        [JsonIgnore]
        public virtual Candidate Candidate { get; }
        [JsonIgnore]

        public virtual Job Job { get; set; }
        [JsonIgnore]

        public virtual CandidateApplyJobStatus CandidateApplyJobStatus { get;}
        [JsonIgnore]

        public virtual CandidateApplyJobSource CandidateApplyJobSource { get;}
        [JsonIgnore]
        public virtual ICollection<InterviewSchedule> InterviewSchedules { get;} = new List<InterviewSchedule>();

    }
}
