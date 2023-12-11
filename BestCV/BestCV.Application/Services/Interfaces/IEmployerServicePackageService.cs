using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.EmployerServicePackage;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.EmployerServicePackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerServicePackageService : IServiceQueryBase<int, InsertEmployerServicePackageDTO, UpdateEmployerServicePackageDTO, EmployerServicePackageDTO>
    {
        Task<DionResponse> GetListAggregate();
    }
}
