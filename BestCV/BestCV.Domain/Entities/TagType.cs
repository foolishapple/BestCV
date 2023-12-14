using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class TagType : EntityCommon<int>
    {
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
