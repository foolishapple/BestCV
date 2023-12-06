using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class Voucher : EntityBase<int>
    {
        public string Code { get; set; }
        public int VoucherTypeId { get; set; }
        public int Value { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string Color { get; set; }
        public virtual VoucherType VoucherType { get; set; } = null!;

        public virtual ICollection<EmployerVoucher> EmployerVouchers { get; } = new List<EmployerVoucher>();
    }
}
