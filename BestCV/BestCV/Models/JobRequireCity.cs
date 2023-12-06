using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobRequireCity : EntityBase<long>, IFullTextSearch
    {
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }

        /// <summary>
        /// Mã tỉnh/thành phố
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        public string Search { get; set; } = null!;
        public virtual Job Job { get; } = null!;
        public virtual WorkPlace WorkPlace { get; } = null!;
        public virtual ICollection<JobRequireDistrict> JobRequireDistricts { get; } = new List<JobRequireDistrict>();
    }
}
