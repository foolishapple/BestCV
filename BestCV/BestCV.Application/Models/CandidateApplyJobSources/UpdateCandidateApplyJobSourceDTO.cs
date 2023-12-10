using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateApplyJobSources
{
    public class UpdateCandidateApplyJobSourceDTO : InsertCandidateApplyJobSourceDTO
    {
        /// <summary>
        /// Mã nguồn ứng viên ứng tuyển công việc
        /// </summary>
        public int Id { get; set; }
    }
}
