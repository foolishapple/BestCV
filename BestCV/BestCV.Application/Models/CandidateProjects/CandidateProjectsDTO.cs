using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateProjects
{
    public class CandidateProjectsDTO : EntityBase<long>
    {
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
        public bool IsDeleted { get; set; } = false;
        public bool IsAdded { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
    }
}
