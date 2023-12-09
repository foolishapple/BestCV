using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.RecruitmentCampaigns;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IRecruitmentCampaignRepository : IRepositoryBaseAsync<RecruitmentCampaign,long, JobiContext>
    {
        /// <summary>
        /// Description: list Recruitment Campaign aggregate datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters);
        /// <summary>
        /// Description: Check recruitment campaign name is exitsted
        /// </summary>
        /// <param name="name">recruitment campaign name</param>
        /// <param name="id">recruitment campagin id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, long id);
    }
}
