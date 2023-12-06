using Jobi.Core.Repositories;
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
	public class ExperienceRangeRepository : RepositoryBaseAsync<ExperienceRange, int, JobiContext>, IExperienceRangeRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> unitOfWork;
		public ExperienceRangeRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
		{
			db = _dbContext;
			unitOfWork = _unitOfWork;
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="name">ExperienceRangeName</param>
		/// <param name="id">ExperienceRangeId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsExperienceRangeExistAsync(string name, int id)
		{
			return await db.ExperienceRange.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
		}
	}
}
