using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ISlideRepository : IRepositoryBaseAsync<Slide,int, JobiContext>
    {
        /// <summary>
        /// Description: Check slide name is existed
        /// </summary>
        /// <param name="name">slide name</param>
        /// <param name="id">slide id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        Task<bool> CandidateOrderSortAsync(int ordersort, int id);
        Task<int> MaxSubSort(int ordersort);
        Task<bool> ChangeOrderSort(List<Slide> objs);
        Task<List<Slide>> GetAllSort();
        Task<List<Slide>> ListSlideShowonHomepage();
    }
}
