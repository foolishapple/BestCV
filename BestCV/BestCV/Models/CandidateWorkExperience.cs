using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateWorkExperience : EntityBase<long>
    {
        /// <summary>
        /// Tiêu đề công việc
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// Công ty
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get;set; }
        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public string TimePeriod { get; set; }  
        public string? Description { get;set; }
        public virtual Candidate Candidate { get; }
        
    }
}
