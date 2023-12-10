using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerWallet
{
    public class EmployerWalletDTO
    {
        public long Id { get; set; }
        public int WalletTypeId { get; set; }
        public long EmployerId { get; set; }
        public int Value { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
