using BestCV.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Lịch sử sử dụng ví
    /// </summary>
    public class EmployerWalletHistory : EntityCommon<long>
    {
        public int Amount { get; set; }
        public long EmployerWalletId { get; set; }
        public int WalletHistoryTypeId { get; set; }
        public long? CandidateId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime UpdatedTime { get; set; }
        [JsonIgnore]

        public virtual WalletHistoryType WalletHistoryType { get; } = null!;
        [JsonIgnore]

        public virtual EmployerWallet EmployerWallet { get;  } = null!;
        [JsonIgnore]
        public virtual Candidate Candidate { get;} = null!;
    }
}
