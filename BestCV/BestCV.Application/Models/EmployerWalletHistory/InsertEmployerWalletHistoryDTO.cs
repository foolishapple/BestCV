using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerWalletHistory
{
    public class InsertEmployerWalletHistoryDTO
    {
        public int Amount { get; set; }
        public long EmployerWalletId { get; set; }
        public int WalletHistoryTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CandidateId { get; set; }
    }
}
