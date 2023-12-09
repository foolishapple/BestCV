using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateSuggestionWorkPlaceRepository : RepositoryBaseAsync<CandidateSuggestionWorkPlace, int, JobiContext>, ICandidateSuggestionWorkPlaceRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateSuggestionWorkPlaceRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CandidateSuggestionWorkPlace> GetByCandidateWorkPlaceIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionWorkPlaces.Where(x => x.Active && x.CandidateId == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Author: ThanhND
        /// CreatedTime : 08/08/2023
        /// Description : Lấy danh sách gợi ý địa điểm làm việc bằng mã ứng viên
        /// </summary>
        /// <param name="id">Mã ứng viên</param>
        /// <returns></returns>
        public async Task<List<CandidateSuggestionWorkPlace>> ListByCandidateIdAsync(long id)
        {
            return await dbContext.CandidateSuggestionWorkPlaces.Where(x=>x.Active && x.CandidateId == id).ToListAsync();
        }
    }
}
