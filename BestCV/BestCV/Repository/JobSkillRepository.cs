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
	public class JobSkillRepository : RepositoryBaseAsync<JobSkill, int, JobiContext>, IJobSkillRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> unitOfWork;
		public JobSkillRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
		{
			db = _dbContext;
			unitOfWork = _unitOfWork;
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="name">JobSkillName</param>
		/// <param name="id">JobSkillId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsJobSkillExistAsync(string name, int id)
		{
			return await db.JobSkill.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
		}
	}
}
