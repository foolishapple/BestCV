using BestCV.Application.Models.MustBeInterestedCompany;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.MustBeInterestedCompany;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IMustBeInterestedCompanyService : IServiceQueryBase<long, InsertMustBeInterestedCompanyDTO, UpdateMustBeInterestedCompanyDTO, MustBeInterestedCompanyDTO>
    {
        Task<BestCVResponse> ListAggregatesAsync();
        Task<List<SelectListItem>> ListCompanySelected();
        Task<BestCVResponse> ListCompanyInterestedOnDetailJob();
    }
}
