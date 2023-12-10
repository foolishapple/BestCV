using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Slides
{
    public class SlideDTO : EntityCommon<int>
    {
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Image { get; set; } = null!;
        /// <summary>
        /// Thứ tự chính sắp xếp tại màn hình ứng viên
        /// </summary>
        public int CandidateOrderSort { get; set; }
        /// <summary>
        /// Thứ tự sắp xếp phụ giữa những silde cùng bậc
        /// </summary>
        public int SubOrderSort { get; set; }
       
    }
}
