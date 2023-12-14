using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CVTemplate : EntityCommon<long>
    {
        /// <summary>
        /// CVTemplateStatus Id
        /// </summary>
        public long CVTemplateStatusId { get; set; }

        /// <summary>
        /// Ảnh Template
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// Phiên bản
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Nội dung HTML của template
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// CSS bổ sung của Template
        /// </summary>
        public string AdditionalCSS { get; set; } = null!;

        public virtual CVTemplateStatus CVTemplateStatus { get; }

        public virtual ICollection<CandidateCV> CandidateCVs { get; } = new List<CandidateCV>();
    }
}
