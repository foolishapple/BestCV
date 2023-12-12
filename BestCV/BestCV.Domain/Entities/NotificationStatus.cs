using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class NotificationStatus : EntityCommon<int>
    {
        public string Color { get; set; } = null!;

        public virtual ICollection<EmployerNotification> EmployerNotifications { get; } = new List<EmployerNotification>();

        public ICollection<CandidateNotification> CandidateNotifications { get; } = new List<CandidateNotification>();
    }
}
