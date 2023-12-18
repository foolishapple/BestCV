using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Services
{

    /// <summary>
    /// Interface service base
    /// </summary>
    /// <typeparam name="T">Type of id column</typeparam>
    /// <typeparam name="K">Insert DTO</typeparam>
    /// <typeparam name="L">Update DTO</typeparam>
    /// <typeparam name="J">Detail DTO</typeparam>
    public interface IServiceQueryBase<T, K, L, J> where K : class where L : class where J : class
    {
        Task<BestCVResponse> GetByIdAsync(T id);
        Task<BestCVResponse> CreateAsync(K obj);
        Task<BestCVResponse> CreateListAsync(IEnumerable<K> objs);
        Task<BestCVResponse> UpdateAsync(L obj);
        Task<BestCVResponse> UpdateListAsync(IEnumerable<L> obj);
        Task<BestCVResponse> SoftDeleteAsync(T id);
        Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<T> objs);
        Task<BestCVResponse> GetAllAsync();
    }
}
