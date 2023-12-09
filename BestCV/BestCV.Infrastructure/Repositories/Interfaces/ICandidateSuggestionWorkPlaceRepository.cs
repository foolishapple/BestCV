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
    public interface ICandidateSuggestionWorkPlaceRepository : IRepositoryBaseAsync<CandidateSuggestionWorkPlace, int, JobiContext>
    {
        Task<CandidateSuggestionWorkPlace> GetByCandidateWorkPlaceIdAsync(long id);

        /// Description : Lấy danh sách gợi ý địa điểm làm việc bằng mã ứng viên
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionWorkPlace>> ListByCandidateIdAsync(long id);
    }
}
