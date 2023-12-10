using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.TopFeatureJob
{
    public class InsertTopFeatureJobDTO
    {
        public long JobId { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
    }
}
