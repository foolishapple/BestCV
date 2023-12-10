using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.MenuType
{
    public class MenuTypeDTO
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
