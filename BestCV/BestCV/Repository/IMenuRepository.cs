using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Menu;
using Jobi.Domain.Aggregates.Post;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IMenuRepository : IRepositoryBaseAsync<Menu, int, JobiContext>
    {
        
        Task<bool> IsMenuExistAsync(string name, int id, int menuTypeId);
        Task UpdateTreeIdsMenu(Menu obj);
        Task<List<MenuAggregate>> ListMenuHomepage();
    }
}
