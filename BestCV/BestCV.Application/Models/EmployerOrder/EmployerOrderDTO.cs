using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.Company;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class EmployerOrderDTO
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
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// Tin đã được duyệt hay chưa
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// Thời hạn đơn hàng
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }
        public List<EmployerDTO> ListEmployer { get; set; } = new List<EmployerDTO>();
        public List<CompanyDTO> ListCompany { get; set; } = new List<CompanyDTO>();
    }
}
