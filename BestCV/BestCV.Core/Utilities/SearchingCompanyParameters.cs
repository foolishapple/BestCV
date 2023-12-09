using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Utilities
{
    public class SearchingCompanyParameters
    {
        public string Keywords { get; set; } = "";
        public int WorkPlace { get; set; } = 0;
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public string CompanySize { get; set; } = "";
        public int FieldOfActivity { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public long CandidateId { get; set; } = 0;
    }
}
