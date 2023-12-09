using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
	public class SalaryTypeRepository : RepositoryBaseAsync<SalaryType, int, JobiContext>, ISalaryTypeRepository
	{
		private readonly JobiContext db;
		private readonly IUnitOfWork<JobiContext> unitOfWork;
		public SalaryTypeRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
		{
			db = _dbContext;
			unitOfWork = _unitOfWork;
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="name">SalaryTypeName</param>
		/// <param name="id">SalaryTypeId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsSalaryTypeExistAsync(string name, int id)
		{
			return await db.SalaryType.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
		}
	}
}
