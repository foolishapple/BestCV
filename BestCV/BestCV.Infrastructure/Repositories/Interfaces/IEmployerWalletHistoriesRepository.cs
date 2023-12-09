using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerWalletHistories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerWalletHistoriesRepository : IRepositoryBaseAsync<EmployerWalletHistory, long, JobiContext>
    {
        /// <summary>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> ListEmployerWalletHistories(DTParameters parameters);
        Task<bool> QuickIsApprovedAsync(long id);
        Task<EmployerWalletHistoriesAggregate> GetByidAggregateAsync(long id);
    }
}
