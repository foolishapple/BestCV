using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.FieldOfActivity;
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
	public class FieldOfActivityRepository : RepositoryBaseAsync<FieldOfActivity, int, JobiContext>, IFieldOfActivityRepository
	{
		private readonly JobiContext _db;
		private readonly IUnitOfWork<JobiContext> _unitOfWork;
		public FieldOfActivityRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
		{
			_db = db;
			_unitOfWork = unitOfWork;
		}
		/// <summary>
		/// Author: TUNGTD
		/// Createad: 11/08/2023
		/// Description: Check Field Of Activity name is existed
		/// </summary>
		/// <param name="name">Field Of Activity name</param>
		/// <param name="id">Field Of Activity id</param>
		/// <returns></returns>
		public async Task<bool> NameIsExisted(string name, int id)
		{
			return await _db.FieldOfActivities.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
		}

		/// <summary>
		/// Author: HuyDQ
		/// Created: 16/08/2023
		/// Description: lấy dữ liệu và đếm xem có bảo nhiêu nhà tuyển dụng có lĩnh vực này
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public async Task<object> LoadDataFilterFielOfActivityHomePageAsync()
        {
			var fieldOfActivityData = await(from f in _db.FieldOfActivities
									from cf in _db.CompanyFieldOfActivities
									where f.Active && cf.Active
									select new FieldOfActivityAggregates
									{
										Id = f.Id,
										Name = f.Name,
										CountActivity = (from row in _db.FieldOfActivities
													from row1 in _db.CompanyFieldOfActivities
													where row.Id == row1.FieldOfActivityId && row.Active && row1.Active && row1.FieldOfActivityId == f.Id
													select row1).Count()
									}).Distinct().ToListAsync();
			return fieldOfActivityData;
		}
	}
}
