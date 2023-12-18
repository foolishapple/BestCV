using BestCV.Application.Models.Coupon;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface ICouponService : IServiceQueryBase<int, InsertCouponDTO, UpdateCouponDTO,CouponDTO>
	{
        Task<BestCVResponse> ListAggregatesAsync();

    }
}
