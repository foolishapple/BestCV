using BestCV.Application.Models.CandidateFollowCompany;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateFollowCompany;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateFollowCompanyService : IServiceQueryBase<long, InsertCandidateFollowCompanyDTO, UpdateCandidateFollowCompanyDTO, CandidateFollowCompanyDTO>
    {
        Task<CandidateFollowCompany> GetAsyncByCandidateIdAndCompanyI(long candidateId, int companyId);

        public Task<BestCVResponse> InsertCandidateWithViewModel(InsertCandidateFollowCompanyDTO candidateDTO, long candidateId);

        Task<BestCVResponse> GetListCompanyByCandidateId(long candidateId);

        Task<BestCVResponse> HardDeleteAsync(long id);

        Task<DTResult<CandidateFollowCompanyAggregates>> PagingByCandidateId(DTPagingCandidateFollowCompanyParameters parameters);
    }
}
