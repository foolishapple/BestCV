using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerBenefitService : IServiceQueryBase<int, InsertEmployerBenefitDTO, UpdateEmployerBenefitDTO, EmployerBenefitDTO>
    {
        Task<DionResponse> GetListBenefitExept(List<int> listData);
    }
}
