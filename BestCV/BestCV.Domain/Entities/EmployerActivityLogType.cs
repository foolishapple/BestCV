using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class EmployerActivityLogType : EntityCommon<int>
    {
        public virtual ICollection<EmployerActivityLog> EmployerActivityLogs { get;} = new List<EmployerActivityLog>();
    }
}
