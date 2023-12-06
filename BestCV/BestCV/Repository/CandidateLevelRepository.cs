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
    public class CandidateLevelRepository : RepositoryBaseAsync<CandidateLevel, int, JobiContext>, ICandidateLevelRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateLevelRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 01/08/2023
        /// </summary>
        /// <param name="name">candidateLevelName</param>
        /// <param name="id">candidateLevelId</param>
        /// <returns>True if exitsted,opposite false </returns>
        public async Task<bool> IsCandidateLevelExistAsync(string name, int id)
        {
            return await db.CandidateLevels.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
        }
    }
}
