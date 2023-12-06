using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.RecruitmentCampaigns;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IRecruitmentCampaignRepository : IRepositoryBaseAsync<RecruitmentCampaign,long, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/08/2023
        /// Description: list Recruitment Campaign aggregate datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: Check recruitment campaign name is exitsted
        /// </summary>
        /// <param name="name">recruitment campaign name</param>
        /// <param name="id">recruitment campagin id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, long id);
    }
}
