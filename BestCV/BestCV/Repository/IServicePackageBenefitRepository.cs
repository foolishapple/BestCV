using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.ServicePackageBenefit;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IServicePackageBenefitRepository : IRepositoryBaseAsync<ServicePackageBenefit, int, JobiContext>
    {
        Task<List<ServicePackageBenefitAggregate>> ListAggregateByServicePackageId(int servicePackageId);
    }
}
