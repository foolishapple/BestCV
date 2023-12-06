using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Permissions;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class PermissionRepository : RepositoryBaseAsync<Permission, int, JobiContext>, IPermissionRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public PermissionRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check permission CODE is existed
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CODEIsExisted(string code, int id)
        {
            return await _db.Permissions.AnyAsync(c => c.Code == code && c.Active && c.Id != id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get permission detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PermissionAggregate> Detail(int id)
        {
            var query = from p in _db.Permissions
                        where p.Active
                        select new PermissionAggregate()
                        {
                            Code = p.Code,
                            Description = p.Description,
                            Id = p.Id,
                            Name = p.Name,
                            Roles = (from rp in _db.RolePermissions
                                     join r in _db.Roles on rp.RoleId equals r.Id
                                     where rp.Active && r.Active
                                     select r.Id).ToHashSet()
                        };
            var data = await query.FirstAsync(c => c.Id == id);
            return data;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check permission name is existed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> NameIsExisted(string name, int id)
        {
            return _db.Permissions.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
