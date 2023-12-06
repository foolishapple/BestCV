using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateFollowCompany;
using Jobi.Domain.Aggregates.CandidateSaveJob;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateFollowCompanyRepository : IRepositoryBaseAsync<CandidateFollowCompany, long, JobiContext>
    {
        public Task<bool> IsCompanyIdExist(long candidateId, int companyId);
        Task<CandidateFollowCompany> GetAsyncByCandidateIdAndCompanyI(long candidateId, int companyId);

        Task<List<CandidateFollowCompanyAggregates>> ListCandidateFollowCompanyByCandidateId(long candidateId);

        Task<DTResult<CandidateFollowCompanyAggregates>> PagingByCandidateId(DTPagingCandidateFollowCompanyParameters parameters);
    }
}
