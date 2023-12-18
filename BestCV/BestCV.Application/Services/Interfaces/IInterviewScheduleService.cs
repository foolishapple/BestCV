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

        public Task<BestCVResponse> GetListByCandidateId(long candidateId);

        public Task<BestCVResponse> GetListByEmployerId(long employerId);
    }
}
