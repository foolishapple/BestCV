using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerCart;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerCartRepository : IRepositoryBaseAsync<EmployerCart,long,JobiContext>
    {
        Task<List<EmployerCartAggregates>> ListByEmployerId(long employerId);
    }
}
