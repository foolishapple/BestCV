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

        Task<BestCVResponse> GroupEmployerService(DTEmployerServicePackageEmployerParameters parameters);

        Task<BestCVResponse> GroupEmployerServiceAddOn(DTEmployerServicePackageEmployerParameters parameters);
    }
}
