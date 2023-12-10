using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerOrder
{
    public class EmployerOrderAggregates
    {
        public long Id { get; set; }
        public int OrderStatusId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string OrderStatusName { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public long EmployerId { get; set; }
        public string EmployerName { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountVoucher { get; set; }
        public int FinalPrice { get; set; }
        public string? TransactionCode { get; set; }
        public string? Info { get; set; }
        public string RequestId { get; set; }
        public string Search { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApplyEndDate { get; set; }
        public string OrderStatusColor { get; set; }
        public List<EmployerOrderDetailAggregates> ListOrderDetail { get; set; }
    }
}
