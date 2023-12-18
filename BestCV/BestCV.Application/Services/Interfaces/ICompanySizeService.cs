using BestCV.Application.Models.CompanySize;
using BestCV.Application.Models.CouponType;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICompanySizeService : IServiceQueryBase<int, InsertCompanySizeDTO, UpdateCompanySizeDTO, CompanySizeDTO>
    {
        public Task<BestCVResponse> LoadDataFilterCompanySizeHomePageAsync();
    }
}
