using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobTag : EntityBase<long>
    {
        /// <summary>
        /// mã thẻ
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }

        public virtual Job Job { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
