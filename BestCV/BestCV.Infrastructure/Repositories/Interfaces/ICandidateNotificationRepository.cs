using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateNotification;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateNotificationRepository : IRepositoryBaseAsync<CandidateNotification,long,JobiContext>
    {

        Task<DTResult<CandidateNotification>> ListCandidateNotificationByIdAsync(CandidateNotificationParameter parameters, long candidateId);
        Task MakeAsRead(CandidateNotification obj);
        Task<int> CountByCandidateId(long candidateId, long? statusId);
        Task<List<CandidateNotification>> ListRecented(long id);
    }
}
