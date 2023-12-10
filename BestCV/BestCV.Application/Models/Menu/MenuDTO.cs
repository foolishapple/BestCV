using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Menu
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int MenuTypeId { get; set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public string TreeIds { get; set; } = null!;
        public bool Active { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public HashSet<int> Roles { get; set; } = null!;
    }
}
