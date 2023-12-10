using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.MustBeInterestedCompany
{
    public class MustBeInterestedCompanyAggregates : EntityBase<long>
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyCoverPhoto { get; set; }
        public int CountJob { get; set; }
        public int CountFollower { get; set; }
        public List<CompanyFieldOfActivityAggregates> CompanyFields { get; set; }
    }
}
