using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Models.WorkPlace;
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
    public interface IWorkplaceService : IServiceQueryBase<int, InsertWorkplaceDTO, UpdateWorkplaceDTO, WorkPlace>
    {
        Task<bool> GetProvinceDataAsync();

        Task<DionResponse> GetListDistrictByCityIdAsync(int cityId);

        Task<DionResponse> GetListCityAsync();
    }
}
