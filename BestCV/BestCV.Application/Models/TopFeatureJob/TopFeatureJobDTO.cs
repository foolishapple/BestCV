using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.TopFeatureJob
{
    public class TopFeatureJobDTO
    {
        public int Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
