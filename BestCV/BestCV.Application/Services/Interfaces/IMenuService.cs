using BestCV.Application.Models.JobSkill;
using BestCV.Application.Models.Menu;
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
    public interface IMenuService : IServiceQueryBase<int, InsertMenuDTO, UpdateMenuDTO, MenuDTO>
    {
        Task<DionResponse> GetListMenuHomepage();
        Task<DionResponse> GetAllMenuAdmin();
        Task<DionResponse> GetAllMenuDashboardEmployer();
        Task<DionResponse> GetAllMenuDashboardCandidate();
        Task<DionResponse> GetAllMenuHeaderCandidate();
    }
}
