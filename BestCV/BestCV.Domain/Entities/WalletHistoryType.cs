using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Loại lịch sử
    /// </summary>
    public class WalletHistoryType : EntityCommon<int>
    {
        /// <summary>
        /// Màu
        /// </summary>
        public string? Color { get; set; }
        public virtual ICollection<EmployerWalletHistory> EmployerWalletHistories { get; } = new List<EmployerWalletHistory>();
    }
}
