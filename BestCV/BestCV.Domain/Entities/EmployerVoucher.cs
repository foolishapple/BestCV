using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class EmployerVoucher :EntityBase<long>
    {
        public long EmployerId { get; set; }

        public int VoucherId { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual Voucher Voucher { get; set; }

        public virtual ICollection<EmployerOrderVoucher> EmployerOrderVouchers { get; } = new List<EmployerOrderVoucher>();

    }
}
