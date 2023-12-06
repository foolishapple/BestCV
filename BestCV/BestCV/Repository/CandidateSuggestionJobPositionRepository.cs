using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CandidateSuggestionJobPositionRepository : RepositoryBaseAsync<CandidateSuggestionJobPosition, long, JobiContext>, ICandidateSuggestionJobPositionRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateSuggestionJobPositionRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CandidateSuggestionJobPosition> GetByCandidateJobPositionIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionJobPositions.Where(x => x.Active && x.CandidateId == id).FirstOrDefaultAsync();

        }
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý vị trí công việc bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        public async Task<List<CandidateSuggestionJobPosition>> ListByCandidateIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionJobPositions.Where(x=>x.Active && x.CandidateId == id).ToListAsync();
        }
    }
}
