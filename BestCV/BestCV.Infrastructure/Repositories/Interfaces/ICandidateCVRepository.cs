using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.CandidateCVs;
using BestCV.Domain.Aggregates.CVTemplate;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateCVRepository : IRepositoryBaseAsync<CandidateCV, long, JobiContext>
    {
        Task<List<CandidateCV>> GetListAsyncByCandidateId(long candidateId);

        Task<List<CandidateCVAggregate>> GetListAggregateAsyncByCandidateId(long candidateId);
    }
}
