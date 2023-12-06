using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.EmployerNotification;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerNotificationRepository : IRepositoryBaseAsync<EmployerNotification, long, JobiContext>
    {
        Task<DTResult<EmployerNotification>> ListEmployerNotificationByEmployerIdAsync(EmployerNotificationParameter parameters, long employerId);
        Task MakeAsRead(EmployerNotification obj);
        Task<List<EmployerNotification>> ListRecented(long id);
        Task<int> CountByEmployerId(long employerId, long? statusId);
    }
}
