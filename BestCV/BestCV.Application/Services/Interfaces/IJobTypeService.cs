using BestCV.Application.Models.JobType;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobTypeService :IServiceQueryBase<int,InsertJobTypeDTO,UpdateJobTypeDTO,JobType>
    {

    }
}
