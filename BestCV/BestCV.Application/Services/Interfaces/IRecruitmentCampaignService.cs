using BestCV.Application.Models.RecruitmentCampaigns;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.RecruitmentCampaigns;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IRecruitmentCampaignService : IServiceQueryBase<long, InsertRecruitmentCampaignDTO, UpdateRecruitmentCampaignDTO, RecruitmentCampaignDTO>
    {

        Task<BestCVResponse> ListToEmployer(long id);

        Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters);

        Task<BestCVResponse> ChangeApproved(ChangeApproveRecruitmentCampaignDTO obj);

        Task<BestCVResponse> ListOpenedByEmployer(long id);
    }
}
