using BestCV.Application.Models.RolePermissions;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IRolePermissionService : IServiceQueryBase<int,InsertRolePermissionDTO,UpdateRolePermissionDTO, RolePermissionDTO>
    {

        Task<BestCVResponse> UpdateList(UpdateListRolePermissionDTO obj);
    }
}
