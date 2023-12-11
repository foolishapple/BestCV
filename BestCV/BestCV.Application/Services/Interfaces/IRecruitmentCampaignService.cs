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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 16/08/2023
        /// Description: List Recruitment Campaign to employer
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        Task<DionResponse> ListToEmployer(long id);
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
        /// Creaed: 22/08/2023
        /// Description: change approved to recruitment campain
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> ChangeApproved(ChangeApproveRecruitmentCampaignDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: List recruitment campaign opened to employer
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        Task<DionResponse> ListOpenedByEmployer(long id);
    }
}
