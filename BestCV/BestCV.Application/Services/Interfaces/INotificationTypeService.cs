using BestCV.Application.Models.NotificationType;
using BestCV.Application.Models.OrderStatus;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface INotificationTypeService : IServiceQueryBase<int, InsertNotificationTypeDTO, UpdateNotificationTypeDTO, NotificationTypeDTO>
    {
    }
}
