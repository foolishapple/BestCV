using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class Company : EntityFullTextSearch<int>
    {
        public long EmployerId { get; set; }
        public int CompanySizeId { get; set; }

        /// <summary>
        /// Địa chỉ của công ty
        /// </summary>
        public string AddressDetail { get; set; } = null!;

        /// <summary>
        /// Địa chỉ của công ty theo workplace
        /// </summary>
        public int WorkPlaceId { get; set; }

        /// <summary>
        /// Tọa độ trên bản đồ
        /// </summary>
        public string? Location { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Website { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string TaxCode { get; set; } = null!;

        /// <summary>
        /// Năm thành lập
        /// </summary>
        public int FoundedIn { get; set; }

        public string? TiktokLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? LinkedinLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? VideoIntro { get; set; }
        public string? Overview { get; set; }

        public virtual ICollection<CompanyImage> CompanyImages { get; } = new List<CompanyImage>();

        public virtual ICollection<CompanyFieldOfActivity> CompanyFieldOfActivities { get; } = new List<CompanyFieldOfActivity>();

        public virtual Employer Employer { get; } = null!;
        public virtual WorkPlace WorkPlace { get; } = null!;
        public virtual CompanySize CompanySize { get; } = null!;

        public virtual ICollection<CompanyReview> CompanyReview { get; set; } = new List<CompanyReview>();

        public virtual TopCompany TopCompany { get; } = null!;

        public virtual ICollection<License> Licenses { get; } = new List<License>();

        public ICollection<CandidateFollowCompany> CandidateFollowCompanies { get;} = new List<CandidateFollowCompany>();
        public virtual ICollection<MustBeInterestedCompany> MustBeInterestedCompanies { get; } = new List<MustBeInterestedCompany>();

    }
}
