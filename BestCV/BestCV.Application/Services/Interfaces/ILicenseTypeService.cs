using BestCV.Application.Models.LicenseType;
using BestCV.Application.Models.Occupation;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ILicenseTypeService : IServiceQueryBase<int, InsertLicenseTypeDTO, UpdateLicenseTypeDTO, LicenseTypeDTO>
    {
    }
}
