using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class EmployerOrderVoucher: EntityBase<long>
    {
        public long EmployerOrderId { get; set; }
        public long VoucherId { get; set; }

        public virtual EmployerOrder EmployerOrder { get; set; } = null!;
        public virtual EmployerVoucher EmployerVoucher { get; set; } = null!;
    }
}
