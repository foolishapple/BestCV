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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 09/08/2023
        /// Description: 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> UpdateList(UpdateListRolePermissionDTO obj);
    }
}
