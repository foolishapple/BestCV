using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerWallet
{
    public class InsertEmployerWalletDTO
    {
        public int WalletTypeId { get; set; }
        public long EmployerId { get; set; }
        public int Value { get; set; }
    }
}
