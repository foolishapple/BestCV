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
    public class SalaryRangeRepository : RepositoryBaseAsync<SalaryRange, int, JobiContext>, ISalaryRangeRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public SalaryRangeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;

        }

        public async Task<bool> IsSalaryRangeExistAsync(string name, int id)
        {
            return await db.SalaryRange.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);

        }
    }
}
