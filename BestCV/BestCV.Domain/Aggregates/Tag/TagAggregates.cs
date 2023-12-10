using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Tag
{
    public class TagAggregates : EntityBase<int>
    {
        public int TagTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string TagTypeName { get; set; }
    }
}
