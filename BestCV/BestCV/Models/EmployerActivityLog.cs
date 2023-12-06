using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class EmployerActivityLog : EntityBase<long>
    {
        /// <summary>
        /// Mã nhà tuyển dụng
        /// </summary>
        public long EmployerId { get; set; }
        /// <summary>
        /// Mã loại lịch sử hoạt động của nhà tuyển dụng
        /// </summary>
        public int EmployerActivityLogTypeId { get; set; }
        /// <summary>
        /// Giá trị cũ
        /// </summary>
        public string? OldValue { get; set; } = null!;
        /// <summary>
        /// Giá trị mới
        /// </summary>
        public string? NewValue { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = null!;
        /// <summary>
        /// Hệ điều hành
        /// </summary>
        public string? OS { get; set; } = null!;
        /// <summary>
        /// Tác nhân người dùng
        /// </summary>
        public string? UserAgent { get; set; } = null!;
        /// <summary>
        /// Trình duyệt
        /// </summary>
        public string? Browser { get; set; } = null!;
        /// <summary>
        /// Địa chỉ IP
        /// </summary>
        public string? IpAddress { get; set; } = null!;
        public virtual EmployerActivityLogType EmployerActivityLogType { get; set; } = null!;
    }
}
