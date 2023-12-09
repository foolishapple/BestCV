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
