using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateSuggestionWorkPlaceRepository : IRepositoryBaseAsync<CandidateSuggestionWorkPlace, int, JobiContext>
    {
        Task<CandidateSuggestionWorkPlace> GetByCandidateWorkPlaceIdAsync(long id);
        /// <summary>
        /// Author: ThanhND
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý địa điểm làm việc bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionWorkPlace>> ListByCandidateIdAsync(long id);
    }
}
