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
    public interface ICandidateSuggestionJobSkillRepository : IRepositoryBaseAsync<CandidateSuggestionJobSkill, long, JobiContext>
    {
        Task<CandidateSuggestionJobSkill> GetByCandidateJobSkillIdAsync(long id);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý kỹ năng bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionJobSkill>> ListByCandidateIdAsync(long id);
    }
}
