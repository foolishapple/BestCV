using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IRecruitmentCampaignStatusRepository : IRepositoryBaseAsync<RecruitmentCampaignStatus, int, JobiContext>
    {
        /// <summary>
        /// Description: Check Recruitment Campaign Status name is existed
        /// </summary>
        /// <param name="name">Recruitment Campaign Status name</param>
        /// <param name="id">Recruitment Campaign Status id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Description: Check Recruitment Campaign Status color is existed
        /// </summary>
        /// <param name="color">Recruitment Campaign Status color</param>
        /// <param name="id">Recruitment Campaign Status id</param>
        /// <returns></returns>
        Task<bool> ColorIsExisted(string color, int id);
    }
}
