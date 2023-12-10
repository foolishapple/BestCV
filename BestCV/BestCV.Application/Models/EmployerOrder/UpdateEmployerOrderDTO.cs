using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class UpdateEmployerOrderDTO 
    {
        public long Id { get; set; }
        public int OrderStatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public long EmployerId { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountVoucher { get; set; }
        public int FinalPrice { get; set; }
        public string? TransactionCode { get; set; }
        public string? Info { get; set; }
        public string RequestId { get; set; }
        public string Search { get; set; } = null!;
        /// <summary>
        /// Thời hạn đơn hàng
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }
    }
}
