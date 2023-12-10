using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Slides
{
    public class InsertSlideDTO
    {
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Image { get; set; } = null!;
        /// <summary>
        /// Thứ tự sắp xếp ở trang ứng viên
        /// </summary>
        public int CandidateOrderSort { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        public int SubOrderSort { get; set; }
    }
}
