using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Coupon;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
	public class CouponRepository : RepositoryBaseAsync<Coupon, int, JobiContext>, ICouponRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> unitOfWork;
		public CouponRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
		{
			db = _dbContext;
			unitOfWork = _unitOfWork;
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="code">CouponCode</param>
		/// <param name="id">CouponId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsCouponExistAsync(string code, int id)
		{
			return await db.Coupon.AnyAsync(c => c.Active && c.Code.ToLower().Trim() == code.ToLower().Trim() && c.Id != id);

		}

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 31/07/2023
        /// </summary>
        /// <returns>List CouponAggregates</returns>
        public async Task<List<CouponAggregates>> ListAggregatesAsync()
        {
			return await (from row in db.Coupon
							  from ct in db.CouponType
							  where row.Active && ct.Active && row.CouponTypeId == ct.Id
							  select new CouponAggregates()
							  {
								  Id = row.Id,
								  Active = row.Active,
								  Code = row.Code,
								  CouponTypeId = row.CouponTypeId,
								  CouponTypeName = ct.Name,
								  CreatedTime = row.CreatedTime,
								  EfficiencyTime = row.EfficiencyTime,
							  }).ToListAsync();
        }
    }
}
