using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerNotification;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerNotificationRepository : IRepositoryBaseAsync<EmployerNotification, long, JobiContext>
    {
        Task<DTResult<EmployerNotification>> ListEmployerNotificationByEmployerIdAsync(EmployerNotificationParameter parameters, long employerId);
        Task MakeAsRead(EmployerNotification obj);
        Task<List<EmployerNotification>> ListRecented(long id);
        Task<int> CountByEmployerId(long employerId, long? statusId);
    }
}
