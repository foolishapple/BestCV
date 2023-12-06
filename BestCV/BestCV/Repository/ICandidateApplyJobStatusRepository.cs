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
    public interface ICandidateApplyJobStatusRepository : IRepositoryBaseAsync<CandidateApplyJobStatus,int,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Check candidate appply job status name is existed
        /// </summary>
        /// <param name="name">candidate apply job status name</param>
        /// <param name="id">candidate apply job id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Check candidate appply job status name is existed
        /// </summary>
        /// <param name="color">candidate apply job status color</param>
        /// <param name="id">candidate apply job status id</param>
        /// <returns></returns>
        Task<bool> ColorIsExisted(string color, int id);
    }
}
