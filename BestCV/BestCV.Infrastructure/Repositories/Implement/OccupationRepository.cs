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
    public class OccupationRepository : RepositoryBaseAsync<Occupation, int, JobiContext>, IOccupationRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public OccupationRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// author: TrungHieuTr
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsNameExisAsync(string name, int id)
        {
            return await db.Occupation.AnyAsync(c => c.Active && c.Name.ToLower() == name.ToLower().Trim() && c.Id != id);
        }
    }
}
