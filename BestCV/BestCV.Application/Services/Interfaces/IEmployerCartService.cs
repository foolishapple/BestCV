using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.EmployerCarts;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerCartService : IServiceQueryBase<long, InsertEmployerCartDTO, UpdateEmployerCartDTO, EmployerCartDTO>
    {
        Task<int> CountServicePackageInCart(long employerId);
        Task<BestCVResponse> ListByEmployerId(long employerId);
        Task<BestCVResponse> AddToCart(int servicePackageId, long employerId);
    }
}
