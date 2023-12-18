using AutoMapper;
using BestCV.Application.Models.CompanySize;
using BestCV.Application.Models.CouponType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CompanySizeService : ICompanySizeService
    {
        private readonly ICompanySizeRepository companySizeRepository;
        private readonly ILogger<ICompanySizeService> logger;
        private readonly IMapper mapper;
        public CompanySizeService(ICompanySizeRepository _companySizeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            companySizeRepository = _companySizeRepository;
            logger = loggerFactory.CreateLogger<CompanySizeService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertCompanySizeDTO obj)
        {
            var data = mapper.Map<CompanySize>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await companySizeRepository.IsCompanySizeExistAsync(data.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await companySizeRepository.CreateAsync(data);
            await companySizeRepository.SaveChangesAsync();
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCompanySizeDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await companySizeRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CompanySizeDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await companySizeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CompanySizeDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await companySizeRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await companySizeRepository.SaveChangesAsync();
                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateCompanySizeDTO obj)
        {
            var data = await companySizeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var result = mapper.Map(obj, data);
            result.Description = !string.IsNullOrEmpty(result.Description) ? result.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await companySizeRepository.IsCompanySizeExistAsync(result.Name, result.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await companySizeRepository.UpdateAsync(result);
            await companySizeRepository.SaveChangesAsync();
            return BestCVResponse.Success(result);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCompanySizeDTO> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : lấy ra thông tin của quy mô công ty và số lượng công ty thuộc quy mô này
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<BestCVResponse> LoadDataFilterCompanySizeHomePageAsync()
        {
            var data = await companySizeRepository.LoadDataFilterCompanySizeHomePageAsync();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
