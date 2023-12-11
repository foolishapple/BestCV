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
        /// <summary>
        /// Author: TUNGTD
        /// Creatd: 18/09/2023
        /// Description: Get list job service package by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<DionResponse> ListAggregate(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Descript
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DionResponse> AddServiceToJob(InsertJobServicePackageDTO model);
    }
}
