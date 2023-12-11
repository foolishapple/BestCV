using BestCV.Application.Models.JobCategory;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobCategoryService : IServiceQueryBase<int, InsertJobCategoryDTO, UpdateJobCategoryDTO, JobCategoryDTO>
    {

    }
}
