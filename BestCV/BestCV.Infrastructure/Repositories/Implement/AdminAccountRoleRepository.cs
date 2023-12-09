using BestCV.Core.Repositories;
using BestCV.Domain.Constants;
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
    public class AdminAccountRoleRepository :RepositoryBaseAsync<AdminAccountRole,long,JobiContext> ,IAdminAccountRoleRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public AdminAccountRoleRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> IsAdminRole(long id)
        {
            return await _db.AdminAccountRoles.AnyAsync(c => c.Active && c.AdminAccountId == id && AdminAccountConst.ADMIN_ROLE.Contains(c.RoleId));
        }
    }
}
