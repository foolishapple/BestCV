using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.TopJobUrgent
{
    public class TopJobUrgentDTO
    {
        public long Id { get; set; }
        public int SubOrderSort { get;set; }
        public long JobId { get; set; }
        public int OrderSort { get; set; }
        public bool Active { get;set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
