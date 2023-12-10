using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateFollowCompany
{
    public class CandidateFollowCompanyAggregates
    {
        public long Id { get; set; }    

        public long CandidateId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        
        public string Phone { get;set; }

        public int CompanySizeId { get; set; }

        public string CompanySizeName { get; set; }

        public string EmailAddressCompany { get;set; }

        public string AddressDetail { get; set; }   
        public string Website { get; set; }   
        public string Logo { get; set; }   

        public DateTime CreatedTime { get; set; }
        
        public int CountJob { get; set; }
    }
}
