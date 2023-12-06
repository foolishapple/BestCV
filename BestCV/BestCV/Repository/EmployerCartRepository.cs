using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerCart;
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
    public class EmployerCartRepository : RepositoryBaseAsync<EmployerCart, long, JobiContext>, IEmployerCartRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public EmployerCartRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }

        public async Task<List<EmployerCartAggregates>> ListByEmployerId(long employerId)
        {
            return await (from row in dbContext.EmployerCarts
                          join e in dbContext.Employers on row.EmployerId equals e.Id
                          join esp in dbContext.EmployerServicePackages on row.EmployerServicePackageId equals esp.Id
                          where row.Active && e.Active && esp.Active && row.EmployerId == employerId
                          select new EmployerCartAggregates()
                          {
                              Id = row.Id,
                              Active = row.Active,
                              EmployerServicePackageId = row.EmployerServicePackageId,
                              EmployerId = row.EmployerId,
                              CreatedTime = row.CreatedTime,
                              Description = row.Description,
                              EmployerName = e.Fullname,
                              Quantity = row.Quantity,
                              EmployerServicePackageName = esp.Name,
                              Price = esp.Price,
                              EmployerServicePackgeDescription =  esp.Description
                          }).ToListAsync();
        }
    }
}
