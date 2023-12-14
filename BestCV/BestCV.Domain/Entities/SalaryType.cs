using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class SalaryType : EntityCommon<int>
    {
        public virtual ICollection<Job> Jobs { get; } = new List<Job>();
    }
}
