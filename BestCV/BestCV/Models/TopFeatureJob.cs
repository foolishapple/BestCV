using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class TopFeatureJob : EntityBase<int>
    {
        public long JobId { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public virtual Job Job { get; } = null!;
    }
}
