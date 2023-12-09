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
    public interface ICandidateApplyJobStatusRepository : IRepositoryBaseAsync<CandidateApplyJobStatus,int,JobiContext>
    {

        /// Description: Check candidate appply job status name is existed
        /// <param name="name">candidate apply job status name</param>
        /// <param name="id">candidate apply job id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);

        /// Description: Check candidate appply job status name is existed
        /// <param name="color">candidate apply job status color</param>
        /// <param name="id">candidate apply job status id</param>
        /// <returns></returns>
        Task<bool> ColorIsExisted(string color, int id);
    }
}
