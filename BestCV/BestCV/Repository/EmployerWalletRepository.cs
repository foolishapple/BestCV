﻿using Jobi.Core.Repositories;
using Jobi.Domain.Constants;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class EmployerWalletRepository : RepositoryBaseAsync<EmployerWallet, long, JobiContext>, IEmployerWalletRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerWalletRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime: 29/09/2023
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task<EmployerWallet> GetCreditWalletByEmployerId(long employerId)
        {
            return await db.EmployerWallets.Where(x => x.Active && x.EmployerId == employerId&& x.WalletTypeId == EmployerWalletConstants.CREDIT_TYPE).FirstOrDefaultAsync();
        }
    }
}
