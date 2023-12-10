using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Company
{
    public class CompanyAggregates
    {
        public int Id { get; set; }
        public long EmployerId { get; set; }
        public int CompanySizeId { get; set; }
        public string CompanySizeName { get; set; }
        public string AddressDetail { get; set; }
        public int WorkPlaceId { get; set; }
        public string WorkPlaceName { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string CoverPhoto { get; set; }
        public string TaxCode { get; set; }
        public int FoundedIn { get; set; }
        public string? FacebookLink { get; set; }
        public string? LinkedinLink { get; set; }
        public string? TwitterLink { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Search { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CountJob { get; set; }
        public string? Overview { get; set; }
        public List<CompanyFieldOfActivityAggregates> companyFieldOfActivityAggregates { get; set; }
        public bool IsFollowedCompany { get; set; }
    }
}
