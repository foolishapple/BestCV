using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateCV : EntityBase<long>
    {
        /// <summary>
        /// Tên CV
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// ID ứng viên
        /// </summary>
        public long CandidateId { get; set; }

        /// <summary>
        /// Id template
        /// </summary>
        public long? CVTemplateId { get; set; }

        /// <summary>
        /// HTML của CV
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// CSS bổ sung của CV
        /// </summary>
        public string AdditionalCSS { get; set; } = null!;

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        public virtual Candidate Candidate { get; } = null!;
        public virtual CVTemplate CVTemplate { get; } = null!;
    }
}
