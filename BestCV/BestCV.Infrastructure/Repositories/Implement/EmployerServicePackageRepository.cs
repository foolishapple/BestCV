using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.EmployerServicePackage;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerServicePackageRepository : RepositoryBaseAsync<EmployerServicePackage, int, JobiContext>, IEmployerServicePackageRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerServicePackageRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> IsEmployerServicePackageRepositoryExist(int id, string name)
        {
            return await db.EmployerServicePackages.AnyAsync(x => x.Active && x.Name.ToLower().Trim() == name.ToLower().Trim() && x.Id != id);
        }

        public async Task<List<EmployerServicePackageAggregate>> GetListAggregate()
        {
            var data = (from row in db.EmployerServicePackages
                        join spg in db.ServicePackageGroups on row.ServicePackageGroupId equals spg.Id
                        join spt in db.ServicePackageTypes on row.ServicePackageTypeId equals spt.Id
                        where row.Active && spg.Active && spt.Active
                        select new EmployerServicePackageAggregate()
                        {
                            Id = row.Id,
                            Name = row.Name,
                            Active = row.Active,
                            ServicePackageTypeId = row.ServicePackageTypeId,
                            ServicePackageGroupId = row.ServicePackageGroupId,
                            CreatedTime = row.CreatedTime,
                            Description = row.Description,
                            DiscountEndDate = row.DiscountEndDate,
                            DiscountPrice = row.DiscountPrice,
                            Price = row.Price,
                            ServicePackageGroupName = spg.Name,
                            ServicePackageTypeName = spt.Name,
                        });
            return await data.ToListAsync();
        }
    }
}
