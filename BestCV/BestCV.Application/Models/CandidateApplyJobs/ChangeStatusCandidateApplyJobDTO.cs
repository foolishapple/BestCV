using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateApplyJobs
{
    public class ChangeStatusCandidateApplyJobDTO
    {
        /// <summary>
        /// Mã ứng viên ứng tuyển công việc
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã trạng thái ứng tuyển
        /// </summary>
        public string CandidateApplyJobStatusId { get; set; } = null!;
    }
}
