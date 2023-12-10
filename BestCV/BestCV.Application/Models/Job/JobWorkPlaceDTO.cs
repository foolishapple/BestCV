using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Job
{
    public class JobWorkPlaceDTO
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }

        public int? DistrictId { get; set; }

        public string? AddressDetail { get; set; }

        /// <summary>
        /// Id của bảng JobRequireCity
        /// </summary>
        public long JobRequireCityId { get; set; }

        /// <summary>
        /// Id của bảng JobRequireDistrict
        /// </summary>
        public long JobRequireDistrictId { get; set; }

        public long JobId { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
    }
}
