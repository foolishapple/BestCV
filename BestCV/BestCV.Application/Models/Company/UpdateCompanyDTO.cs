using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Company
{
    public class UpdateCompanyDTO
    {
        public int id { get; set; }
        public long EmployerId { get; set; }
        public int CompanySizeId { get; set; }
        public string EmailAddress { get; set; }

        /// <summary>
        /// Địa chỉ của công ty
        /// </summary>
        public string AddressDetail { get; set; } = null!;

        /// <summary>
        /// Địa chỉ của công ty theo tỉnh thành
        /// </summary>
        public int WorkPlaceId { get; set; }

        /// <summary>
        /// Tọa độ trên bản đồ
        /// </summary>
        public string Location { get; set; }

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
        public string? Description { get; set; }
        public string? Overview { get; set; }
    }
}
