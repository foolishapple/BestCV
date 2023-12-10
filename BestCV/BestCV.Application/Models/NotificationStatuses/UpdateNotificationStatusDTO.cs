using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.NotificationStatuses
{
    public class UpdateNotificationStatusDTO : InsertNotificationStatusDTO
    {
        /// <summary>
        /// Mã trạng thái thông báo
        /// </summary>
        public int Id { get; set; }
    }
}
