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

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : Get create license
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertLicenseDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            await licenseRepository.CreateAsync(license);
            await licenseRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertLicenseDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : Get all license
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await licenseRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<List<LicenseDTO>>(data);
            return DionResponse.Success(model);
        }

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : Get by id license
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await licenseRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<LicenseDTO>(data);
            return DionResponse.Success(model);
        }

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : Get list by id companyid
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetListByCompanyId(int companyId)
        {
            var data = await licenseRepository.GetListByCompanyId(companyId);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }     
            return DionResponse.Success(data);
        }

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : soft delete license
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var data = await licenseRepository.SoftDeleteAsync(id);
            if (data)
            {
                await licenseRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }

            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 04/08/2023
        /// Description : update license
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateLicenseDTO obj)
        {
            List<string> listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var license = await licenseRepository.GetByIdAsync(obj.Id);
            if (license == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, license);
            license.Search = obj.CompanyId + "_" + obj.LicenseTypeId + "_" +
                             obj.Path + "_" + obj.IsApproved + "_" +
                             obj.ApprovalDate + "_" + license.CreatedTime;
            await licenseRepository.UpdateAsync(model);
            await licenseRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateLicenseDTO> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 07/09/2023
        /// Description : Lấy danh sách giầy tờ nhà tuyển dụng (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        public async Task<Object> ListLicenseAggregates(DTParameters parameters)
        {
            return await licenseRepository.ListLicenseAggregates(parameters);
        }

        public async Task<DionResponse> UpdateApproveStatusLicenseAsync(ApproveLicenseDTO obj)
        {
            var data = await licenseRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }

            var updateObj = mapper.Map(obj, data);

            if (updateObj.IsApproved)
            {
                updateObj.ApprovalDate = DateTime.Now;
            }

            if (await licenseRepository.UpdateApproveStatusLicenseAsync(updateObj))
            {

                return DionResponse.Success("Duyệt bài viết thành công.");
            }
            else
            {
                return DionResponse.Error("Duyệt bài viết không thành công.");

            }

        }
    }
}
