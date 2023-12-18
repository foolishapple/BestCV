using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class TopCompany : EntityBase<int>
    {
        public int CompanyId { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public virtual Company Company { get; } = null!;
    }
}
