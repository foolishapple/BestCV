using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateFollowCompany
{
    public class CandidateFollowCompanyDTO
    {
        public long id { get;set; }
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Mã công ty
        /// </summary>
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
