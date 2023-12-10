using BestCV.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerWalletHistories
{
    public  class EmployerWalletHistoriesAggregate
    {
        public long Id { get; set; }
        public int Amount { get; set; }
        public long EmployerWalletId { get; set; }
        public int WalletHistoryTypeId { get; set; }
        public long? CandidateId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime UpdatedTime { get; set; }
        
        public long EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string? EmployerPhone { get; set; }
        public string EmployerEmail { get; set; }
        public string CandidateName { get; set;}
        public string? CandidatePhone { get; set; }
        public string? CandidateEmail { get;set; }
        
        public string CompanyName { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; } = null!;

    }
}
