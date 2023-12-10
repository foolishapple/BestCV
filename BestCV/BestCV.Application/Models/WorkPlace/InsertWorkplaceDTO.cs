using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.WorkPlace
{
    public class InsertWorkplaceDTO
    {
        /// <summary>
        /// Tên tỉnh thành/quận huyện
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } 

        /// <summary>
        /// ID Tỉnh thành nếu đây là quận huyện
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Mã tỉnh thành
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Loại đơn vị hành chính
        /// </summary>
        public string DivisionType { get; set; } = null!;

        /// <summary>
        /// Tên đơn vị hành chính viết ở dạng snake_case và bỏ dấu
        /// </summary>
        public string CodeName { get; set; } = null!;

        /// <summary>
        /// Mã vùng điện thoại tỉnh thành
        /// </summary>
        public int? PhoneCode { get; set; }

        /// <summary>
        /// Mã tỉnh thành (chỉ quận huyện mới có)
        /// </summary>
        public int? ProvinceCode { get; set; }
    }
}
