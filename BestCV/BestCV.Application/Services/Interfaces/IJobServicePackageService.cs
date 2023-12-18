using BestCV.Application.Models.JobServicePackages;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.JobServicePackages;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobServicePackageService : IServiceQueryBase<JobServicePackage,InsertJobServicePackageDTO,UpdateJobServicePackageDTO,JobServicePackageDTO>
    {

        Task<BestCVResponse> ListAggregate(long id);

        Task<BestCVResponse> AddServiceToJob(InsertJobServicePackageDTO model);
    }
}
