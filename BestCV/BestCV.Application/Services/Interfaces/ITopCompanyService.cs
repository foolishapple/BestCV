using BestCV.Application.Models.TopCompany;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITopCompanyService : IServiceQueryBase<int, InsertTopCompanyDTO, UpdateTopCompanyDTO, TopCompanyDTO>
    {
        Task<BestCVResponse> ListTopCompanyShowOnHomePageAsync();
        Task<List<SelectListItem>> ListCompanySelected();
        Task<BestCVResponse> ListCompany();
        Task<BestCVResponse> ChangeOrderSort(ChangeTopCompanyDTO model);
    }
}
