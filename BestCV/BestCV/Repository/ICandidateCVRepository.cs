using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CandidateCVs;
using Jobi.Domain.Aggregates.CVTemplate;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateCVRepository : IRepositoryBaseAsync<CandidateCV, long, JobiContext>
    {
        Task<List<CandidateCV>> GetListAsyncByCandidateId(long candidateId);

        Task<List<CandidateCVAggregate>> GetListAggregateAsyncByCandidateId(long candidateId);
    }
}
