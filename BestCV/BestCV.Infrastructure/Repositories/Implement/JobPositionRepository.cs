using BestCV.Core.Repositories;
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
	public class JobPositionRepository : RepositoryBaseAsync<JobPosition, int, JobiContext>, IJobPositionRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> unitOfWork;
		public JobPositionRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
		{
			db = _dbContext;
			unitOfWork = _unitOfWork;
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="name">JobPositionName</param>
		/// <param name="id">JobPositionId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsJobPositionExistAsync(string name, int id)
		{
			return await db.JobPosition.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
		}
	}
}
