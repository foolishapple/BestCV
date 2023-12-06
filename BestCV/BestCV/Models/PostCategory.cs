﻿using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class PostCategory:EntityCommon<int>
    {
        public int ParrentId { get; set; }
        public string Color { get; set; }
        public virtual ICollection<Post> Posts { get; } = new List<Post>();
    }
}
