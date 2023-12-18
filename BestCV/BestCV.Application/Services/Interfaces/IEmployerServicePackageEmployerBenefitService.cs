using BestCV.Application.Models.EmployerServicePackageEmployerBenefit;
using BestCV.Application.Validators.EmployerServicePackageEmployerBenefit;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerServicePackageEmployerBenefitService : IServiceQueryBase<int, InsertEmployerServicePackageEmployerBenefitDTO, UpdateEmployerServicePackageEmployerBenefitDTO, EmployerServicePackageEmployerBenefitDTO>
    {

        Task<BestCVResponse> GetByEmployerServicePackageIdAsync(int id);

        Task<BestCVResponse> UpdateHasBenefitAsync(int id);
    }
}
