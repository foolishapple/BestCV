using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateNotifications
{
    public class CandidateNotificationDTO
    {
        public string Name { get; set; } 
        public long CandidateId {get; set; }
        public string Description { get; set; }
        public long NotifycationTypeId { get; set; }
        public string NotifycationTypeName { get; set; }
        
        public long NotifiCationStatusId { get; set; }

        public string NotifiCationStatusName { get; set; }

        public string? Link {get ; set; }
        public DateTime CreatedTime { get; set; }
    }
}
