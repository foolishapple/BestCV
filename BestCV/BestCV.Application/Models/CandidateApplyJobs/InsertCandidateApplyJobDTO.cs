using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateApplyJobs
{
    public class InsertCandidateApplyJobDTO
    {
        /// <summary>
        /// Mã ứng viên
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
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
