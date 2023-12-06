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
    public class EmployerActivityLogTypeRepository : RepositoryBaseAsync<EmployerActivityLogType, int, JobiContext>, IEmployerActivityLogTypeRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public EmployerActivityLogTypeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check employer activity log type name is existed
        /// </summary>
        /// <param name="name">employer activity log type name</param>
        /// <param name="id">employer activity log type id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.EmployerActivityLogTypes.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
