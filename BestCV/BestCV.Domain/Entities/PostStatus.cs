using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class PostStatus : EntityCommon<int>
    {
        public string Color { get; set; }
        public virtual ICollection<Post> Posts { get; } = new List<Post>();

    }
}
