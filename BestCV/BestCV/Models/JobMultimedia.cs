using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobMultimedia : EntityBase<long>, IFullTextSearch
    {
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }

        /// <summary>
        /// Đường dẫn tệp tin đa phương tiện
        /// </summary>
        public string Path { get; set; } = null!;

        /// <summary>
        /// Mã loại tệp tin đa phương tiện
        /// </summary>
        public int MultimediaTypeId { get; set; }

        /// <summary>
        /// Trường tìm kiếm
        /// </summary>
        public string Search { get; set; } = null!;

        public virtual Job Job { get;}
        public virtual MultimediaType MultimediaType { get; }
    }
}
