using BestCV.Application.Models.Menu;
using BestCV.Application.Models.MenuType;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IMenuTypeService : IServiceQueryBase<int, InsertMenuTypeDTO, UpdateMenuTypeDTO, MenuTypeDTO>
    {
        Task<DionResponse> GetMenuTypeAsync();
    }
}
