using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateSaveJob
{
    public class CandidateSaveJobAggregates
    {
        public long Id { get; set; }
        public long CandidateId { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public DateTime CreatedTime { get;set; }
        public List<string> CityRequired { get; set; }
        public string CompanyAddress { get; set; } = null!;
        public string CompanyPhone { get; set; } = null!;
        public string CompanyWebsite { get; set; } = null!;
        public int SalaryTypeId { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public DateTime? JobApplyEndDate { get; set; }
    }
}
