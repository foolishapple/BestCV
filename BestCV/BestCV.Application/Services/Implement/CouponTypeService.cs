using AutoMapper;
using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.Menu;
using BestCV.Application.Models.SalaryType;
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
    public class CouponTypeService : ICouponTypeService
    {
        private readonly ICouponTypeRepository couponTypeRepository;
        private readonly ILogger<ICouponTypeService> logger;
        private readonly IMapper mapper;
        public CouponTypeService(ICouponTypeRepository _couponTypeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            couponTypeRepository = _couponTypeRepository;
            logger = loggerFactory.CreateLogger<ICouponTypeService>();
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertCouponTypeDTO obj)
        {
            var couponType = mapper.Map<CouponType>(obj);
            couponType.Active = true;
            couponType.CreatedTime = DateTime.Now;
            couponType.Description = !string.IsNullOrEmpty(couponType.Description) ? couponType.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await couponTypeRepository.IsCouponTypeExistAsync(couponType.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await couponTypeRepository.CreateAsync(couponType);
            await couponTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCouponTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <returns>BestCVResponse</returns>
        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await couponTypeRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CouponTypeDTO>>(data);
            return BestCVResponse.Success(result);
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponTypeId</param>
        /// <returns>BestCVResponse</returns>
        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await couponTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CouponTypeDTO>(data);

            return BestCVResponse.Success(result);
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponTypeId</param>
        /// <returns>BestCVResponse</returns>
        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await couponTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await couponTypeRepository.SaveChangesAsync();
                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="obj">InsertCouponTypeDTO</param>
        /// <returns>BestCVResponse</returns>
        public async Task<BestCVResponse> UpdateAsync(UpdateCouponTypeDTO obj)
        {
            var couponType = await couponTypeRepository.GetByIdAsync(obj.Id);
            if (couponType == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateCoupon = mapper.Map(obj, couponType);
            updateCoupon.Description = !string.IsNullOrEmpty(updateCoupon.Description) ? updateCoupon.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await couponTypeRepository.IsCouponTypeExistAsync(updateCoupon.Name, updateCoupon.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await couponTypeRepository.UpdateAsync(updateCoupon);
            await couponTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCouponTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
