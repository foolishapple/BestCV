using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class TopAreaJob : EntityBase<int>
    {
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// Thự tự sắp xếp mở rộng
        /// </summary>
        public int SubOrderSort { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public virtual Job Job { get; } = null!;
    }
}
