using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CompanyFieldOfActivity;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
	public class CompanyFieldOfActivityRepository : RepositoryBaseAsync<CompanyFieldOfActivity, int, JobiContext>, ICompanyFieldOfActivityRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> _unitOfWork;
		public CompanyFieldOfActivityRepository(JobiContext _db, IUnitOfWork<JobiContext> unitOfWork) : base(_db, unitOfWork)
		{
			db = _db;
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Author: HuyDQ
		/// CreatedTime : 11/08/2023
		/// Description : Lấy danh sách lĩnh vực hoạt động của công ty theo companyid
		/// </summary>
		/// <param name="companyId">CompanyId</param>
		/// <returns>object</returns>
		public async Task<List<CompanyFieldOfActivityAggregates>> GetFieldActivityByCompanyId(int companyId)
        {
            var result = await(
                from cf in db.CompanyFieldOfActivities
                join c in db.Companies on cf.CompanyId equals c.Id
                join f in db.FieldOfActivities on cf.FieldOfActivityId equals f.Id


                where c.Active && cf.Active && f.Active && cf.CompanyId == companyId
                select new CompanyFieldOfActivityAggregates
				{
					Id = cf.Id,
                    CompanyId = companyId,
					FieldOfActivityId = f.Id,
					FieldOfActivityName = f.Name,
					Active = cf.Active,
					CreatedTime = cf.CreatedTime
                }).ToListAsync();

            return result;
        }

		/// <summary>
		/// Author: HuyDQ
		/// CreatedTime : 11/08/2023
		/// Description : Kiểm tra lĩnh vực hoạt động của công ty có tồn tại không
		/// </summary>
		/// <param></param>
		/// <returns>object</returns>
		public async Task<bool> IsExisted(int id, int companyId, int fieldOfActivityId)
        {
			return await db.CompanyFieldOfActivities.AnyAsync(e => e.CompanyId == companyId && 
																 e.FieldOfActivityId == fieldOfActivityId && 
																 e.Id != id &&
																 e.Active);
		}

		/// <summary>
		/// Author: HuyDQ
		/// CreatedTime : 14/08/2023
		/// Description : xóa lĩnh vực hoạt động của công ty
		/// </summary>
		/// <param></param>
		/// <returns>object</returns>
		public async Task<bool> SoftDeleteByCompanyIdAsync(int companyId)
        {
			var objs = await db.CompanyFieldOfActivities.Where(c => c.Active && c.CompanyId == companyId).ToListAsync();
			if (objs != null && objs.Count > 0)
			{
				foreach (var obj in objs)
				{
					obj.Active = false;

					db.Attach(obj);
					db.Entry(obj).Property(x => x.Active).IsModified = true;
				}
			}
			return true;
		}
	}
}
