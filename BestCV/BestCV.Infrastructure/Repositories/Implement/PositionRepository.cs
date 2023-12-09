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
    public class PositionRepository : RepositoryBaseAsync<Position, int, JobiContext>, IPositionRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public PositionRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }

        public async Task<bool> IsNameExistAsync(string name, int id)
        {
            return await dbContext.Positions.AnyAsync( c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);
        }

    }
}
