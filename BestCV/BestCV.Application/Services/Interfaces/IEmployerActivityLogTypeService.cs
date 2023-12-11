using BestCV.Application.Models.EmployerActivityLogTypes;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerActivityLogTypeService : IServiceQueryBase<int, InsertEmployerActivityLogTypeDTO, UpdateEmployerActivityLogTypeDTO, EmployerActivityLogTypeDTO>
    {
    }
}
