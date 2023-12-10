using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Aggregates.CandidateApplyJobs
{
    public class CandidateApplyJobAggregate
    {

        public long Id { get; set; }

        public long EmployerId { get; set; }

        public bool IsEmployerViewed { get; set; }

        public string CandidateAvatar { get; set; } = null!;

        public string CandidateName { get; set; } = null!;

        public string RecruimentCampaignName { get; set; } = null!;

        public long RecruimentCampaignId { get; set; }

        public string CandidateEmail { get; set; } = null!;

        public string CandidatePhone { get; set; } = null!;

        public int CandidateApplyJobSourceId { get; set; }

        public string CandidateApplyJobSourceName { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        public int CandidateApplyJobStatusId { get; set; }

        public string CandidateApplyJobStatusName { get; set; } = null!;

        public string CandidateApplyJobStatusColor { get; set; } = null!;

        public string? Description { get; set; }

        public string JobName { get; set; } = null!;
        public long CandidateCVPDFId { get; set; }
        public long CandidateId { get; set; }
        public string CandidateCVPDFUrl { get; set; } = null!;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long JobId { get; set; }
        public List<string> CityRequired { get; set; }
        public string CompanyAddress { get; set; } = null!;
        public string CompanyPhone { get; set; } = null!;
        public string CompanyWebsite { get; set; } = null!;
        public int SalaryTypeId { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public string CandidateAddress { get; set; }
    }
}
