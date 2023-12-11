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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get permission detail by id
        /// </summary>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        Task<DionResponse> Detail(int id);
    }
}
