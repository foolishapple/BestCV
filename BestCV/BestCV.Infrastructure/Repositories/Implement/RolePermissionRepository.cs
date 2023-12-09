using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class RolePermissionRepository : RepositoryBaseAsync<RolePermission,int,JobiContext> ,IRolePermissionRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public RolePermissionRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
    }
}
