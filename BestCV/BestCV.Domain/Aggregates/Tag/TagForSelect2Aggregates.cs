using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Tag
{
    public class TagForSelect2Aggregates : SearchQuery
    {
        public int TagTypeId { get; set; }
    }
}
