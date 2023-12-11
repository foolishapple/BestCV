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
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 10/09/2023
        /// Description : Lấy danh sách quyền lợi với employerServicePackageId
        /// </summary>
        /// <param name="id">employerServicePackageId</param>
        /// <returns></returns>
        Task<DionResponse> GetByEmployerServicePackageIdAsync(int id);

        Task<DionResponse> UpdateHasBenefitAsync(int id);
    }
}
