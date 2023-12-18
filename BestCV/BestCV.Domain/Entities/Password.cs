using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class Password : EntityBase<long>
    {
        /// <summary>
        /// Mã tài khoản 
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Password cũ
        /// </summary>
        public string OldPassword { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
