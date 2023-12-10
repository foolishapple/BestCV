using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateLevel
{
    public class InsertCandidateLevelDTO
    {
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public int ExpiryTime { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
