using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobRequireDistrict : EntityBase<long>, IFullTextSearch
    {
        /// <summary>
        /// Mã tỉnh/thành phố
        /// </summary>
        public long JobRequireCityId { get; set; }

        /// <summary>
        /// Mã quận/huyện
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Địa chỉ chi tiết
        /// </summary>
        public string? AddressDetail { get; set; }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        public string Search { get; set; } = null!;

        public virtual JobRequireCity JobRequireCity { get; } = null!;
    }
}
