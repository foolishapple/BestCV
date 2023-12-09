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
    public interface ICandidateSuggestionJobSkillRepository : IRepositoryBaseAsync<CandidateSuggestionJobSkill, long, JobiContext>
    {
        Task<CandidateSuggestionJobSkill> GetByCandidateJobSkillIdAsync(long id);

        /// Description : Lấy danh sách gợi ý kỹ năng bằng mã ứng viên
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionJobSkill>> ListByCandidateIdAsync(long id);
    }
}
