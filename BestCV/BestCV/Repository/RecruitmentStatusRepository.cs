using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class RecruitmentStatusRepository : RepositoryBaseAsync<RecruitmentStatus, int, JobiContext>, IRecruitmentStatusRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public RecruitmentStatusRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <param name="color">RecruitmentStatusColor</param>
        /// <returns>bool</returns>
        public async Task<bool> IsColorExistAsync(int id, string color)
        {
            return await db.RecruitmentStatuses.AnyAsync(c => c.Color.ToLower().Trim() == color.ToLower().Trim() && c.Id != id && c.Active);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <param name="name">RecruitmentStatusName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistAsync(int id, string name)
        {
            return await db.RecruitmentStatuses.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);

        }
    }
}
