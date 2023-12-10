using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.PostTag
{
    public class PostTagDTO : EntityBase<int>
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

    }
}
