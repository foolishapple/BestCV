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
    public interface ICandidateSuggestionJobPositionRepository : IRepositoryBaseAsync<CandidateSuggestionJobPosition, long, JobiContext>
    {
        Task<CandidateSuggestionJobPosition> GetByCandidateJobPositionIdAsync(long id);
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý vị trí công việc bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        Task<List<CandidateSuggestionJobPosition>> ListByCandidateIdAsync(long id);
    }
}
