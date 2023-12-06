using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IRecruitmentStatusRepository : IRepositoryBaseAsync<RecruitmentStatus, int, JobiContext>
    {
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <param name="name">RecruitmentStatusName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <param name="color">RecruitmentStatusColor</param>
        /// <returns>bool</returns>
        Task<bool> IsColorExistAsync(int id, string color);

    }
}
