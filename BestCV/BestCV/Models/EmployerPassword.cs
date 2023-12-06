using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class EmployerPassword : EntityBase<long>
    {
        public long EmployerId { get; set; }

        public string OldPassword { get; set; } = null!;

        public virtual Employer Employer { get; set; } = null!;
    }
}
