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
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="obj">InsertCouponDTO</param>
		/// <returns>DionResponse.Error("Mã coupon đã tồn tại")</returns>
		/// <returns>DionResponse.Success(obj)</returns>
		/// <returns>DionResponse.Error("Thêm mới coupon không thành công")</returns>
		public async Task<DionResponse> CreateAsync(InsertCouponDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            await couponRepository.CreateAsync(coupon);
			await couponRepository.SaveChangesAsync();
			return DionResponse.Success(obj);
		}

		public Task<DionResponse> CreateListAsync(IEnumerable<InsertCouponDTO> objs)
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
			var data = await couponRepository.FindByConditionAsync(x => x.Active && x.CouponType.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CouponDTO>>(data);
			return DionResponse.Success(result);
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">couponId</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> GetByIdAsync(int id)
		{
			var data = await couponRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CouponDTO>(data);

            return DionResponse.Success(result);	
		}

        public async Task<DionResponse> ListAggregatesAsync()
        {
			var result = await couponRepository.ListAggregatesAsync();
            if (result == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", result);
            }
            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 26/07/2023
        /// </summary>
        /// <param name="id">couponId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
		{
			var data = await couponRepository.SoftDeleteAsync(id);
			if (data)
			{
				await couponRepository.SaveChangesAsync();
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
		/// <param name="obj">UpdateCouponDTO</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> UpdateAsync(UpdateCouponDTO obj)
		{
			var coupon = await couponRepository.GetByIdAsync(obj.Id);
			if (coupon == null)
			{
				return DionResponse.NotFound("Không có dữ liệu", obj);
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
                return DionResponse.BadRequest(listErrors);
            }
            await couponRepository.UpdateAsync(updateCoupon);
			await couponRepository.SaveChangesAsync();
			return DionResponse.Success(obj);
		}

		public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCouponDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
