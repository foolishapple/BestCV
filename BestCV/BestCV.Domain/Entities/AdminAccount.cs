using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class AdminAccount : EntityBase<long>, IFullTextSearch
    {
        /// <summary>
        /// Search không dấu
        /// </summary>
        public string Search { get; set; } = null!;
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get; set; } = null!;
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        /// 
        public string Phone { get; set; } = null!;
        public string? Photo { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; } = null!;
        /// <summary>
        /// Số lần đăng nhập thất bại
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// Bị khoá tài khoản?
        /// </summary>
        public bool LockEnabled { get; set; }
        /// <summary>
        /// Bị khóa đến thời gian nào
        /// </summary>
        public DateTime? LockEndDate { get; set; }
        public virtual ICollection<AdminAccountRole> AdminAccountRoles { get; } = new List<AdminAccountRole>();
        public virtual ICollection<Post> Posts { get; } = new List<Post>();
        public virtual ICollection<UploadFile> UploadFiles { get; } = new List<UploadFile>();
        public virtual ICollection<AdminAccountMeta> AdminAccountMetas { get; } = new List<AdminAccountMeta>();
    }
}
