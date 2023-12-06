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
    public class MenuTypeRepository : RepositoryBaseAsync<MenuType, int, JobiContext>, IMenuTypeRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public MenuTypeRepository(JobiContext _db, IUnitOfWork<JobiContext> _unitOfWork) : base(_db, _unitOfWork)
        {
            db = _db;
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> NameIsExisted(string name, int id)
        {
           return await db.MenuTypes.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
        }
    }
}
