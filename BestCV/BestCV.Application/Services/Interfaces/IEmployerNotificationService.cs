using BestCV.Application.Models.Employer;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerNotification;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerNotificationService : IServiceQueryBase<long, EmployerNotification, EmployerNotification, EmployerNotification>
    {
        Task<List<EmployerNotification>> GetAllByEmployerId(long employerId);
        Task<DTResult<EmployerNotification>> DTPaging(EmployerNotificationParameter parameters, long employerId);
        Task<BestCVResponse> MakeAsRead(long id);
        Task<BestCVResponse> CountUnreadByEmployerId(long id);
        Task<BestCVResponse> ListRecented(long id);

    }
}
