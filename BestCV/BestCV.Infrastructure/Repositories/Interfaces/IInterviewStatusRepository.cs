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
    public interface IInterviewStatusRepository : IRepositoryBaseAsync<InterviewStatus, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">InterviewStatusId</param>
        /// <param name="name">InterviewStatusName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        /// <summary>
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">InterviewStatusId</param>
        /// <param name="color">InterviewStatusColor</param>
        /// <returns>bool</returns>
        Task<bool> IsColorExistAsync(int id, string color);

    }
}
