using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class WalletTypeRepository : RepositoryBaseAsync<WalletType, int, JobiContext>, IWalletTypeRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public WalletTypeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
    }
}
