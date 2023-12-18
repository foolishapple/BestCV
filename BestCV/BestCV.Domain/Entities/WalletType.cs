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
    /// Loại Ví
    /// </summary>
    public class WalletType : EntityCommon<int>
    {
        public virtual ICollection<EmployerWallet> EmployerWallets { get; } = new List<EmployerWallet>();
    }
}
