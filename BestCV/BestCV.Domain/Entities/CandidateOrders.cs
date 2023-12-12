using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateOrders:EntityBase<long>
    {
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã trạng thái đơn hàng
        /// </summary>
        public int OrderStatusId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã phương thức thanh toán
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã học viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giá
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giảm giá
        /// </summary>
        public int DiscountPrice { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Phiếu Giảm giá
        /// </summary>
        public int DiscountCounpon { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Giá niêm yết
        /// </summary>
        public int FinalPrice { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã giao dịch
        /// </summary>
        public string? TransactionCode { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Thông tin giao dịch
        /// </summary>
        public string? Info { get; set; }
        // <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã yêu cầu giao dịch
        /// </summary>
        public string RequestId { get; set; }

        public virtual Candidate Candidate { get; }
        public virtual OrderStatus OrderStatus { get; }
        public virtual PaymentMethod PaymentMethod { get; }

        public ICollection<CandidateOrderDetail> CandidateOrderDetails { get;} = new List<CandidateOrderDetail>();
        public ICollection<CandidateOrderCoupon> CandidateOrderCoupons { get; } = new List<CandidateOrderCoupon>();


    }
}
