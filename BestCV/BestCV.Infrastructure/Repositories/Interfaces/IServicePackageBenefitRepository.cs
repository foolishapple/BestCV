using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.ServicePackageBenefit;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IServicePackageBenefitRepository : IRepositoryBaseAsync<ServicePackageBenefit, int, JobiContext>
    {
        Task<List<ServicePackageBenefitAggregate>> ListAggregateByServicePackageId(int servicePackageId);
    }
}
