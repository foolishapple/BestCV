using BestCV.Application.Models.InterviewStatsus;
using BestCV.Application.Models.MultimediaType;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IInterviewStatusService : IServiceQueryBase<int, InsertInterviewStatusDTO, UpdateInterviewStatusDTO, InterviewStatusDTO>
    {
    }
}
