using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public partial class NotificationType : EntityCommon<int>
    {
        public virtual ICollection<EmployerNotification> EmployerNotifications { get; } = new List<EmployerNotification>();
        public ICollection<CandidateNotification> CandidateNotifications { get;} = new List<CandidateNotification>();
    }
}
