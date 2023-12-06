using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateNotification;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateNotificationRepository : IRepositoryBaseAsync<CandidateNotification,long,JobiContext>
    {

        Task<DTResult<CandidateNotification>> ListCandidateNotificationByIdAsync(CandidateNotificationParameter parameters, long candidateId);
        Task MakeAsRead(CandidateNotification obj);
        Task<int> CountByCandidateId(long candidateId, long? statusId);
        Task<List<CandidateNotification>> ListRecented(long id);
    }
}
