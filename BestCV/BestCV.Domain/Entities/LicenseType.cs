using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class LicenseType : EntityCommon<int>
    {
        public virtual ICollection<License> Licenses { get; } = new List<License>();
    }
}
