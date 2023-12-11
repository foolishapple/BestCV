using BestCV.Application.Models.EmployerServicePackageEmployers;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.EmployerServicePackageEmployers;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces

{
    public interface IEmployerServicePackageEmployerService : IServiceQueryBase<long, InsertEmployerServicePackageEmployerDTO, UpdateEmployerServicePackageEmployerDTO, EmployerServicePackageEmployerDTO>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Get list employer service package 
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> GroupEmployerService(DTEmployerServicePackageEmployerParameters parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Get list employer service package add on
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> GroupEmployerServiceAddOn(DTEmployerServicePackageEmployerParameters parameters);
    }
}
