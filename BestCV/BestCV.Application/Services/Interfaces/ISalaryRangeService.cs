using BestCV.Application.Models.SalaryRange;
using BestCV.Application.Models.SalaryType;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ISalaryRangeService : IServiceQueryBase<int, InsertSalaryRangeDTO, UpdateSalaryRangeDTO, SalaryRangeDTO>
    {
    }
}
