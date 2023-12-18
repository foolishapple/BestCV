using BestCV.Application.Models.Permissions;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IPermissionService : IServiceQueryBase<int, InsertPermissionDTO, UpdatePermissionDTO, PermissionDTO>
    {

        Task<BestCVResponse> Detail(int id);
    }
}
