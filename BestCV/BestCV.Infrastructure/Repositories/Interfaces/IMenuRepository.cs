using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Menu;
using BestCV.Domain.Aggregates.Post;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IMenuRepository : IRepositoryBaseAsync<Menu, int, JobiContext>
    {
        
        Task<bool> IsMenuExistAsync(string name, int id, int menuTypeId);
        Task UpdateTreeIdsMenu(Menu obj);
        Task<List<MenuAggregate>> ListMenuHomepage();
    }
}
