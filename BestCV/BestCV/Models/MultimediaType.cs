using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class MultimediaType : EntityCommon<int>
    {
        public virtual ICollection<JobMultimedia> JobMultimedias { get; } = new List<JobMultimedia>();
    }
}
