using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Menu
{
    public class MenuAggregate
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int? MenuTypeId { get;set; }
        public string MenuTypeName { get;set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Description { get; set; }

    }
}
