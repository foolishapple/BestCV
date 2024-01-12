using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Menu;
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
    public class MenuRepository : RepositoryBaseAsync<Menu, int, JobiContext>, IMenuRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public MenuRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> IsMenuExistAsync(string name, int id, int menuTypeId)
        {
            return await db.Menus.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.MenuTypeId == menuTypeId);
        }

        public async Task<List<MenuAggregate>> ListMenuHomepage()
        {
            return await (from row in db.Menus
                          join mt in db.MenuTypes on row.MenuTypeId equals mt.Id
                          where row.Active
                          && mt.Active
                          && row.MenuTypeId != MenuConstant.DEFAULT_VALUE_MENU_ADMIN
                          orderby row.CreatedTime
                          select new MenuAggregate
                          {
                              Active = row.Active,
                              Id = row.Id,
                              Name = row.Name,
                              MenuTypeId = row.MenuTypeId,
                              MenuTypeName =  mt.Name,
                              CreatedTime = row.CreatedTime,
                              Description = row.Description,
                              Icon = row.Icon,
                              Link = row.Link,
                          }).ToListAsync();
        }

        public async Task UpdateTreeIdsMenu(Menu obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(x => x.TreeIds).IsModified = true;
        }
    }
}
