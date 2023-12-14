using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateOrders:EntityBase<long>
    {
        public int OrderStatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public long CandidateId { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountCounpon { get; set; }
        public int FinalPrice { get; set; }
        public string? TransactionCode { get; set; }
        public string? Info { get; set; }
        public string RequestId { get; set; }

        public virtual Candidate Candidate { get; }
        public virtual OrderStatus OrderStatus { get; }
        public virtual PaymentMethod PaymentMethod { get; }

        public ICollection<CandidateOrderDetail> CandidateOrderDetails { get;} = new List<CandidateOrderDetail>();
        public ICollection<CandidateOrderCoupon> CandidateOrderCoupons { get; } = new List<CandidateOrderCoupon>();


    }
}
