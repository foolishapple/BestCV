using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.InterviewSchedule;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IInterviewScheduleRepository: IRepositoryBaseAsync<InterviewSchedule, int, JobiContext>
    {
        /// <summary>
        /// Description: Check interview Schedule name is existed
        /// </summary>
        /// <param name="name">interview Schedule name</param>
        /// <param name="id">interview Schedule id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);

        /// <summary>
        /// Description: get list InterviewScheduleAggregates by candidateId
        /// </summary>
        /// <param name="candidateId">cadidate id</param>
        /// <returns></returns>
        public Task<List<InterviewScheduleAggregates>> GetListInterViewsByCandidateId(long candidateId);

        /// <summary>
        /// Description: get list InterviewScheduleAggregates by employerId
        /// </summary>
        /// <param name="employerId">cadidate id</param>
        /// <returns></returns>
        public Task<List<InterviewScheduleAggregates>> GetListInterViewsByEmployerId(long employerId);
    }
}
