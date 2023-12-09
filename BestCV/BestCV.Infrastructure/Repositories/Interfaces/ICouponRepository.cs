using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Coupon;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
	public interface ICouponRepository : IRepositoryBaseAsync<Coupon, int, JobiContext>
	{
		Task<bool> IsCouponExistAsync(string code, int id);
		Task<List<CouponAggregates>> ListAggregatesAsync();

	}
}
