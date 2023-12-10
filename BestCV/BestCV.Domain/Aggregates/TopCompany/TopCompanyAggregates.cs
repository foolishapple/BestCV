using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.TopCompany
{
    public class TopCompanyAggregates : EntityBase<int>
    {
        public int TopCompanyId { get; set; }
        public string TopCompanyName { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public long EmployerId { get; set; }
        public int CompanySizeId { get; set; }
        public string CompanySizeName { get; set; }
        public string AddressDetail { get; set; }
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
        public string Name { get; set; }
        public string Search { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public int CountJob { get; set; }
    }
}
