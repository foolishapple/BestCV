using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateNotification : EntityFullTextSearch<long>
    {
        /// <summary>
        /// Mã loại thông báo
        /// </summary>
        public int NotificationTypeId { get; set; }
        /// <summary>
        /// Mã trạng thái thông báo
        /// </summary>
        public int NotificationStatusId { get; set; }
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long? CandidateId { get; set; }
        /// <summary>
        /// Liên kết
        /// </summary>
        public string? Link { get; set; }

        public virtual NotificationStatus NotificationStatus { get; } 
        public virtual NotificationType NotificationType { get; } 
        public virtual Candidate? Candidate { get; }

    }
}
