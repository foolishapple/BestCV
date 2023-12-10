using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateCVs
{
    public class InsertOrUpdateCandidateCVDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

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
    }
}
