using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.EmployerCart;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerCartRepository : IRepositoryBaseAsync<EmployerCart,long,JobiContext>
    {
        Task<List<EmployerCartAggregates>> ListByEmployerId(long employerId);
    }
}
