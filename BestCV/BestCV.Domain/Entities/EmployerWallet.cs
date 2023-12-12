using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    /// <summary>
    /// Ví nhà tuyển dụng
    /// </summary>
    public class EmployerWallet : EntityBase<long>
    {
        /// <summary>
        /// Mã loại ví
        /// </summary>
        public int WalletTypeId { get; set; }
        /// <summary>
        /// Mã nhà tuyển dụng
        /// </summary>
        public long EmployerId { get; set; }
        /// <summary>
        /// Số dư
        /// </summary>
        public int Value { get; set; }
        public virtual WalletType WalletType { get;  } = null!;
        public virtual Employer Employer { get; } = null!;
        public virtual ICollection<EmployerWalletHistory> EmployerWalletHistories { get; } = new List<EmployerWalletHistory>();
    }
}
