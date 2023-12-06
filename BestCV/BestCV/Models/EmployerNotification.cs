using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class EmployerNotification : EntityCommon<long>, IFullTextSearch
    {
        public int NotificationTypeId { get; set; }
        public int NotificationStatusId { get; set; }
        public long? EmployerId { get; set; }
        public string? Link { get; set; }
        public string Search { get ; set ; }

        public virtual NotificationType NotificationType { get; set; } = null!;
        public virtual NotificationStatus NotificationStatus { get; set; } = null!;
        public virtual Employer? Employer { get; set; } = null!;
    }
}
