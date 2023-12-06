using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.EmployerWalletHistories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerWalletHistoriesRepository : IRepositoryBaseAsync<EmployerWalletHistory, long, JobiContext>
    {
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 04/10/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> ListEmployerWalletHistories(DTParameters parameters);
        Task<bool> QuickIsApprovedAsync(long id);
        Task<EmployerWalletHistoriesAggregate> GetByidAggregateAsync(long id);
    }
}
