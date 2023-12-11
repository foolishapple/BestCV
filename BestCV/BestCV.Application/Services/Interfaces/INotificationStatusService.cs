using BestCV.Application.Models.NotificationStatuses;
using BestCV.Application.Models.SystemConfigs;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface INotificationStatusService : IServiceQueryBase<int, InsertNotificationStatusDTO, UpdateNotificationStatusDTO, NotificationStatusDTO>
    {
    }
}
