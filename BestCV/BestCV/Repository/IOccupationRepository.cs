using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IOccupationRepository : IRepositoryBaseAsync<Occupation, int, JobiContext>
    {
        /// <summary>
        /// Author: TrungHieuTr
        /// Created: 26/07/2023
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsNameExisAsync(string name, int id);
    }
}
