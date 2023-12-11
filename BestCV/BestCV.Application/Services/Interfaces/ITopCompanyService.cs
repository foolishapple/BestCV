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
        Task<DionResponse> ListTopCompanyShowOnHomePageAsync();
        Task<List<SelectListItem>> ListCompanySelected();
        Task<DionResponse> ListCompany();
        Task<DionResponse> ChangeOrderSort(ChangeTopCompanyDTO model);
    }
}
