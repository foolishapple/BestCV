using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidatePassword : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }

        /// <summary>
        /// Mật khẩu cũ
        /// </summary>
        public string OldPassword { get; set; }
        public virtual Candidate Candidate { get; }

    }
}
