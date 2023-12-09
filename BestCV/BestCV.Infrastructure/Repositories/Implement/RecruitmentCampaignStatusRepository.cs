using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class RecruitmentCampaignStatusRepository : RepositoryBaseAsync<RecruitmentCampaignStatus, int, JobiContext>, IRecruitmentCampaignStatusRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public RecruitmentCampaignStatusRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check Recruitment Campaign Status name is existed
        /// </summary>
        /// <param name="name">Recruitment Campaign Status name</param>
        /// <param name="id">Recruitment Campaign Status id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.RecruitmentCampaignStatuses.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check Recruitment Campaign Status color is existed
        /// </summary>
        /// <param name="color">Recruitment Campaign Status color</param>
        /// <param name="id">Recruitment Campaign Status id</param>
        /// <returns></returns>
        public async Task<bool> ColorIsExisted(string color, int id)
        {
            return await _db.RecruitmentCampaignStatuses.AnyAsync(c => c.Color == color && c.Active && c.Id != id);
        }
    }
}
