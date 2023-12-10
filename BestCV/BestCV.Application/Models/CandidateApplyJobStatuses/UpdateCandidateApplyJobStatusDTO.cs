using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateApplyJobStatuses
{
    public class UpdateCandidateApplyJobStatusDTO : InsertCandidateApplyJobStatusDTO
    {
        /// <summary>
        /// Mã trình trạng ứng viên ứng tuyển công việc
        /// </summary>
        public int Id { get; set; }
    }
}
