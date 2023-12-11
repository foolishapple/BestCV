using BestCV.Application.Models.JobStatuses;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobStatusService : IServiceQueryBase<int, InsertJobStatusDTO, UpdateJobStatusDTO, JobStatusDTO>
    {
    }
}
