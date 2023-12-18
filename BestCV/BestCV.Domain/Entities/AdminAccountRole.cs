using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class AdminAccountRole:EntityBase<long>
    {
        public long AdminAccountId { get; set; }
        public int RoleId { get; set; }
        public virtual AdminAccount AdminAccount { get;} = null!;
        public virtual Role Role { get;} = null!;
    }
}
