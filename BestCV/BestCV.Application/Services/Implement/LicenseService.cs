using AutoMapper;
using BestCV.Application.Models.License;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository licenseRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ILicenseService> logger;
        public LicenseService(ILicenseRepository _licenseRepository, ILoggerFactory _loggerFactory, IMapper _mapper)
        {
            licenseRepository = _licenseRepository;
            logger = _loggerFactory.CreateLogger<ILicenseService>();
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertLicenseDTO obj)
        {
            var license = mapper.Map<License>(obj);
            license.IsApproved = false;
            license.ApprovalDate = null;
            license.Active = true;
            license.CreatedTime = DateTime.Now;
            license.Search = obj.CompanyId + "_" + obj.LicenseTypeId + "_" +
                             obj.Path + "_" + license.IsApproved + "_" +
                             license.ApprovalDate + "_" + license.CreatedTime;
             
            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await licenseRepository.CreateAsync(license);
            await licenseRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertLicenseDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await licenseRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<List<LicenseDTO>>(data);
            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await licenseRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<LicenseDTO>(data);
            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> GetListByCompanyId(int companyId)
        {
            var data = await licenseRepository.GetListByCompanyId(companyId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }     
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var data = await licenseRepository.SoftDeleteAsync(id);
            if (data)
            {
                await licenseRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }

            return BestCVResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateLicenseDTO obj)
        {
            List<string> listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var license = await licenseRepository.GetByIdAsync(obj.Id);
            if (license == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, license);
            license.Search = obj.CompanyId + "_" + obj.LicenseTypeId + "_" +
                             obj.Path + "_" + obj.IsApproved + "_" +
                             obj.ApprovalDate + "_" + license.CreatedTime;
            await licenseRepository.UpdateAsync(model);
            await licenseRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateLicenseDTO> obj)
        {
            throw new NotImplementedException();
        }


        public async Task<Object> ListLicenseAggregates(DTParameters parameters)
        {
            return await licenseRepository.ListLicenseAggregates(parameters);
        }

        public async Task<BestCVResponse> UpdateApproveStatusLicenseAsync(ApproveLicenseDTO obj)
        {
            var data = await licenseRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }

            var updateObj = mapper.Map(obj, data);

            if (updateObj.IsApproved)
            {
                updateObj.ApprovalDate = DateTime.Now;
            }

            if (await licenseRepository.UpdateApproveStatusLicenseAsync(updateObj))
            {

                return BestCVResponse.Success("Duyệt bài viết thành công.");
            }
            else
            {
                return BestCVResponse.Error("Duyệt bài viết không thành công.");

            }

        }
    }
}
