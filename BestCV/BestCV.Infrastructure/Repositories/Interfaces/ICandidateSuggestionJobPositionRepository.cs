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
    public interface ICandidateSuggestionJobPositionRepository : IRepositoryBaseAsync<CandidateSuggestionJobPosition, long, JobiContext>
    {
        Task<CandidateSuggestionJobPosition> GetByCandidateJobPositionIdAsync(long id);

        /// Description : Lấy danh sách gợi ý vị trí công việc bằng mã ứng viên
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionJobPosition>> ListByCandidateIdAsync(long id);
    }
}
