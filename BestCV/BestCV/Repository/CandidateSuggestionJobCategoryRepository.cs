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
    public class CandidateSuggestionJobCategoryRepository : RepositoryBaseAsync<CandidateSuggestionJobCategory, long, JobiContext>, ICandidateSuggestionJobCategoryRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateSuggestionJobCategoryRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CandidateSuggestionJobCategory> GetByCandidateJobCategoryIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionJobCategories.Where(x => x.Active && x.CandidateId == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý danh mục công việc bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        public async Task<List<CandidateSuggestionJobCategory>> ListByCandidateIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionJobCategories.Where(x=>x.Active && x.CandidateId == id).ToListAsync();
        }
    }
}
