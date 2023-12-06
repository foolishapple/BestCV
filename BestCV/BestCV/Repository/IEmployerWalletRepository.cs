using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerWalletRepository : IRepositoryBaseAsync<EmployerWallet, long, JobiContext>
    {
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime: 29/09/2023
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<EmployerWallet> GetCreditWalletByEmployerId(long employerId);
    }
}
