using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerWalletRepository : IRepositoryBaseAsync<EmployerWallet, long, JobiContext>
    {
        /// <summary>
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<EmployerWallet> GetCreditWalletByEmployerId(long employerId);
    }
}
