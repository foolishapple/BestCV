using AutoMapper;
using BestCV.Application.Models.Coupon;
using BestCV.Application.Models.Menu;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
	public class CouponService : ICouponService
	{
		private readonly ICouponRepository couponRepository;
		private readonly ILogger<ICouponService> logger;
		private readonly IMapper mapper;
		public CouponService(ICouponRepository _couponRepository, ILoggerFactory loggerFactory, IMapper _mapper)
		{
			couponRepository = _couponRepository;
			logger = loggerFactory.CreateLogger<ICouponService>();
			mapper = _mapper;
		}

		public async Task<BestCVResponse> CreateAsync(InsertCouponDTO obj)
		{
			var coupon = mapper.Map<Coupon>(obj);
			coupon.Active = true;
			coupon.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();
            var isNameExist = await couponRepository.IsCouponExistAsync(coupon.Code, 0);
            if (isNameExist)
            {
                listErrors.Add("Mã coupon đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await couponRepository.CreateAsync(coupon);
			await couponRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);
		}

		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCouponDTO> objs)
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
			var data = await couponRepository.FindByConditionAsync(x => x.Active && x.CouponType.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CouponDTO>>(data);
			return BestCVResponse.Success(result);
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">couponId</param>
		/// <returns>BestCVResponse</returns>
		public async Task<BestCVResponse> GetByIdAsync(int id)
		{
			var data = await couponRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CouponDTO>(data);

            return BestCVResponse.Success(result);	
		}

        public async Task<BestCVResponse> ListAggregatesAsync()
        {
			var result = await couponRepository.ListAggregatesAsync();
            if (result == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", result);
            }
            return BestCVResponse.Success(result);
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponId</param>
        /// <returns>BestCVResponse</returns>
        public async Task<BestCVResponse> SoftDeleteAsync(int id)
		{
			var data = await couponRepository.SoftDeleteAsync(id);
			if (data)
			{
				await couponRepository.SaveChangesAsync();
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
		/// <param name="obj">UpdateCouponDTO</param>
		/// <returns>BestCVResponse</returns>
		public async Task<BestCVResponse> UpdateAsync(UpdateCouponDTO obj)
		{
			var coupon = await couponRepository.GetByIdAsync(obj.Id);
			if (coupon == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", obj);
			}
			var updateCoupon = mapper.Map(obj, coupon);

            var listErrors = new List<string>();
            var isNameExist = await couponRepository.IsCouponExistAsync(updateCoupon.Code, updateCoupon.Id);
            if (isNameExist)
            {
                listErrors.Add("Mã coupon đã tồn tại");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await couponRepository.UpdateAsync(updateCoupon);
			await couponRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);
		}

		public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCouponDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
