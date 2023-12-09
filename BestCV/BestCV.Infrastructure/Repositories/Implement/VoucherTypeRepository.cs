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
    public class VoucherTypeRepository : RepositoryBaseAsync<VoucherType, int, JobiContext>, IVoucherTypeRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public VoucherTypeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check voucher typename is existed
        /// </summary>
        /// <param name="name">voucher typename</param>
        /// <param name="id">voucher typeid</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.VoucherTypes.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
