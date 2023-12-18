using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateMeta:EntityCommon<long>
    {
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : giá trị
        /// </summary>
        public string Value { get; set; }

        public virtual Candidate Candidate { get; }
    }
}
