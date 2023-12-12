using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobStatus : EntityCommon<int>
    {
        public string Color { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; } = new List<Job>();
    }
}
