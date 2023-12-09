using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.ServicePackageBenefit;
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
    public class ServicePackageBenefitRepository : RepositoryBaseAsync<ServicePackageBenefit, int, JobiContext>, IServicePackageBenefitRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public ServicePackageBenefitRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<List<ServicePackageBenefitAggregate>> ListAggregateByServicePackageId(int servicePackageId)
        {
            var data = await (from row in db.ServicePackageBenefits
                              from esp in db.EmployerServicePackages
                              from b in db.Benefits
                              where row.Active && esp.Active && b.Active && row.EmployerServicePackageId == esp.Id && row.BenefitId == b.Id && row.EmployerServicePackageId == servicePackageId
                              select new ServicePackageBenefitAggregate
                              {
                                  Id = row.Id,
                                  Active = row.Active,
                                  BenefitId = row.BenefitId,
                                  BenefitName = b.Name,
                                  CreatedTime = row.CreatedTime,
                                  Description = row.Description,
                                  EmployerServiePackageId = row.EmployerServicePackageId,
                                  EmployerServiePackageName = esp.Name,
                                  Value = row.Value,
                              }).ToListAsync();
            return data;
        }
    }
}
