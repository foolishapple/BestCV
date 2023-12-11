using BestCV.Application.Models.InterviewSchdule;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IInterviewScheduleService :  IServiceQueryBase<int, InsertInterviewScheduleDTO, UpdateInterviewScheduleDTO, InterviewScheduleDTO>
    {
        /// <summary>
        /// Author: HuyDQ
        /// Created: 22/08/2023
        /// Description: get list interview Schedule by candidateId
        /// </summary>
        /// <param name="candidateId">cadidate id</param>
        /// <returns></returns>
        public Task<DionResponse> GetListByCandidateId(long candidateId);

        /// <summary>
        /// Author: HuyDQ
        /// Created: 29/08/2023
        /// Description: get list interview Schedule by employerId
        /// </summary>
        /// <param name="employerId">employer id</param>
        /// <returns></returns>
        public Task<DionResponse> GetListByEmployerId(long employerId);
    }
}
