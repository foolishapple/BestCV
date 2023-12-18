using BestCV.Application.Models.License;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ILicenseService : IServiceQueryBase<long, InsertLicenseDTO, UpdateLicenseDTO, LicenseDTO>
    {
        public Task<BestCVResponse> GetListByCompanyId(int companyId);

        public Task<object> ListLicenseAggregates(DTParameters parameters);


        public Task<BestCVResponse> UpdateApproveStatusLicenseAsync(ApproveLicenseDTO obj);
    }
}
