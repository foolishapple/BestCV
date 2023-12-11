using BestCV.Application.Models.CandidateNotifications;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateNotification;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateNotificationService : IServiceQueryBase<long , InsertCandidateNotificationDTO, UpdateCandidateNotificationDTO , CandidateNotificationDTO>
    {
        Task<DTResult<CandidateNotification>> DTPaging(CandidateNotificationParameter parameters, long candidateId);
        Task<DionResponse> MakeAsRead(long id);
       
        Task<DionResponse> CountUnreadByCandidateId(long id);
        Task<DionResponse> ListRecented(long id);
    }
}
