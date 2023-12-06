using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IWorkplaceRepository : IRepositoryBaseAsync<WorkPlace, int, JobiContext>
    {
        Task<List<WorkPlace>> GetListDistrictByCityIdAsync(int cityId);

        Task<List<WorkPlace>> GetListCityAsync();
    }
}
