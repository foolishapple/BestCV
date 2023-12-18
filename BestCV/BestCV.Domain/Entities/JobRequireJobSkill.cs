using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class JobRequireJobSkill : EntityBase<long>
    {
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }

        /// <summary>
        /// Mã kĩ năng yêu cầu
        /// </summary>
        public int JobSkillId { get; set; }

        public virtual Job Job { get; }
        public virtual JobSkill JobSkill { get; }
    }
}
