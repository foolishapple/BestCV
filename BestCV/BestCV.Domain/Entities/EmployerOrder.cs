using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class EmployerOrder : EntityBase<long>, IFullTextSearch
    {
        public int OrderStatusId { get; set; }
        /// <summary>
        /// Mã kiểu thanh toán
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// Mã nhà tuyển dụng
        /// </summary>
        public long EmployerId { get; set; }
        public int Price { get; set; }
        /// <summary>
        /// Giảm giá
        /// </summary>
        public int DiscountPrice { get; set; }
        /// <summary>
        /// Phiếu giảm giá
        /// </summary>
        public int DiscountVoucher { get; set; }
        /// <summary>
        /// Giá cuối
        /// </summary>
        public int FinalPrice { get; set; }
        /// <summary>
        /// Mã giao dịch
        /// </summary>
        public string? TransactionCode { get; set; }
        /// <summary>
        /// Thông tin mua hàng
        /// </summary>
        public string? Info { get; set; }
        /// <summary>
        /// Mã yêu cầu
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// đơn hàng có được duyệt không
        /// </summary>
        public bool IsApproved { get; set; }

        public string Search { get; set; }
        /// <summary>
        /// Thời hạn đơn hàng
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }

        /// <summary>
        /// Thời điểm duyệt tin
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        public virtual OrderStatus EmployerOrderStatus { get; set; } = null!;

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        public virtual Employer Employer { get; set; } = null!;

        public virtual ICollection<EmployerOrderDetail> EmployerOrderDetails { get; } = new List<EmployerOrderDetail>();

        public virtual ICollection<EmployerOrderVoucher> EmployerOrderVouchers { get; } = new List<EmployerOrderVoucher>();
    }
}
