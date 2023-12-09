using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Core.Services
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
        Task<DionResponse> GetByIdAsync(T id);
        Task<DionResponse> CreateAsync(K obj);
        Task<DionResponse> CreateListAsync(IEnumerable<K> objs);
        Task<DionResponse> UpdateAsync(L obj);
        Task<DionResponse> UpdateListAsync(IEnumerable<L> obj);
        Task<DionResponse> SoftDeleteAsync(T id);
        Task<DionResponse> SoftDeleteListAsync(IEnumerable<T> objs);
        Task<DionResponse> GetAllAsync();
    }
}
