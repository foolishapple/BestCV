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
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="obj">InsertCouponTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertCouponTypeDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            await couponTypeRepository.CreateAsync(couponType);
            await couponTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertCouponTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await couponTypeRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CouponTypeDTO>>(data);
            return DionResponse.Success(result);
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await couponTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CouponTypeDTO>(data);

            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await couponTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return DionResponse.Success();
                await couponTypeRepository.SaveChangesAsync();
                return DionResponse.Success();

            }
            return DionResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="obj">InsertCouponTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdateCouponTypeDTO obj)
        {
            var couponType = await couponTypeRepository.GetByIdAsync(obj.Id);
            if (couponType == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
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
                return DionResponse.BadRequest(listErrors);
            }
            await couponTypeRepository.UpdateAsync(updateCoupon);
            await couponTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCouponTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
