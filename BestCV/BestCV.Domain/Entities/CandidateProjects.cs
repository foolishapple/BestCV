using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateProjects : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Tên dự án
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Khách hàng
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Số lượng người trong team
        /// </summary>
        public int TeamSize { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Trách nhiệm
        /// </summary>
        public string Responsibilities { get; set; }
        /// <summary>
        /// Thông tin
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public string TimePeriod { get; set; }
        public virtual Candidate Candidate { get;}

    }
}
