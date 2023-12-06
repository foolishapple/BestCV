using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerServicePackageEmployerBenefit;
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
    public class EmployerServicePackageEmployerBenefitRepository : RepositoryBaseAsync<EmployerServicePackageEmployerBenefit, int, JobiContext>, IEmployerServicePackageEmployerBenefitRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public EmployerServicePackageEmployerBenefitRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<List<EmployerServicePackageEmployerBenefitAggregates>> GetByEmployerServicePackageIdAsync(int id)
        {
            return await (from row in db.EmployerServicePackageEmployerBenefits
                          from esp in db.EmployerServicePackages
                          from eb in db.EmployerBenefits
                          where row.Active && esp.Active && eb.Active
                          && row.EmployerServicePackageId == esp.Id
                          && row.EmployerBenefitId == eb.Id
                          && row.EmployerServicePackageId == id
                          select new EmployerServicePackageEmployerBenefitAggregates
                          {
                              Id = row.Id,
                              Active = row.Active,
                              EmployerServicePackageId = row.EmployerServicePackageId,
                              EmployerServicePackageName = esp.Name,
                              CreatedTime = row.CreatedTime,
                              EmployerBenefitId = row.EmployerBenefitId,
                              EmployerBenefitName = eb.Name,
                              HasBenefit = row.HasBenefit,
                              Value = row.Value
                              
                          }).ToListAsync();
        }

        public async Task<bool> IsExistAsync(int id, int employerPackageId, int benefitId)
        {
            return await db.EmployerServicePackageEmployerBenefits.AnyAsync(x => x.Active && x.Id != id && x.EmployerServicePackageId == employerPackageId && x.EmployerBenefitId == benefitId);
        }
        public async Task<bool> UpdateHasBenefitAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.HasBenefit = obj.HasBenefit ? false : true;

                db.Attach(obj);
                db.Entry(obj).Property(x => x.HasBenefit).IsModified = true;
                return true;
            }
            return false;
        }
    }
}
