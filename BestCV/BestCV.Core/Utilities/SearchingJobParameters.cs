using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Utilities
{
    public class SearchingJobParameters
    {
        public string Keywords { get; set; } = "";
        public int Location { get; set; } = 0;
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public int JobCategoryId { get; set; }
        public string JobType { get; set; } = "";
        public string JobExperience { get; set; } = "";
        public string SalaryRange { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public long CandidateId { get; set; } = 0;
        public string JobPosition { get; set; } = "";
    }
}
