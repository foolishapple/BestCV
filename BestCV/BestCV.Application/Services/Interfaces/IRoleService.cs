using BestCV.Application.Models.Roles;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IRoleService : IServiceQueryBase<int,InsertRoleDTO,UpdateRoleDTO,RoleDTO>
    {
    }
}
