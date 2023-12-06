using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Coupon;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
	public interface ICouponRepository : IRepositoryBaseAsync<Coupon, int, JobiContext>
	{
		Task<bool> IsCouponExistAsync(string code, int id);
		Task<List<CouponAggregates>> ListAggregatesAsync();

	}
}
