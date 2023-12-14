using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CompanyFieldOfActivity : EntityBase<int>
    {
        /// <summary>
        /// Mã công ty
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Lĩnh vực hoạt động
        /// </summary>
        public int FieldOfActivityId { get; set; }

        public virtual Company Company { get; set; }
        public virtual FieldOfActivity FieldOfActivity { get; set; }
    }
}
