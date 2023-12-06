using Jobi.Core.Repositories;
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
    public class RoleRepository : RepositoryBaseAsync<Role,int,JobiContext>,IRoleRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public RoleRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check role CODE is existed
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CODEIsExisted(string code, int id)
        {
            return await _db.Roles.AnyAsync(c => c.Code == code && c.Active && c.Id!=id);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check role name is existed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> NameIsExisted(string name, int id)
        {
            return _db.Roles.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
