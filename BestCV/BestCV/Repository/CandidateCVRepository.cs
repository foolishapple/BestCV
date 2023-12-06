using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CandidateCVs;
using Jobi.Domain.Aggregates.CVTemplate;
using Jobi.Domain.Constants;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CandidateCVRepository : RepositoryBaseAsync<CandidateCV, long, JobiContext>, ICandidateCVRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public CandidateCVRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/09/2023
        /// Description: Lấy danh sách theo CandidateId
        /// </summary>
        /// <param name="candidateId">CandidateId</param>
        /// <returns>Danh sách CandidateCV theo CandidateId</returns>
        public async Task<List<CandidateCV>> GetListAsyncByCandidateId(long candidateId)
        {
            return await (
                from cc in db.CandidateCVs
                join c in db.Candidates on cc.CandidateId equals c.Id
                where c.Active && cc.Active && cc.CandidateId == candidateId
                select cc
            ).ToListAsync();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/09/2023
        /// Description: Lấy danh sách Aggregate theo candidateId
        /// </summary>
        /// <param name="candidateId">candidateId</param>
        /// <returns>Danh sách Aggregate theo candidateId</returns>
        public async Task<List<CandidateCVAggregate>> GetListAggregateAsyncByCandidateId(long candidateId)
        {
            return await (
                from cc in db.CandidateCVs
                join c in db.Candidates on cc.CandidateId equals c.Id
                where cc.Active && c.Active && cc.CandidateId == candidateId
                orderby cc.ModifiedTime descending
                let cvTemplate = (
                    from t in db.CVTemplates
                    where t.Active && t.CVTemplateStatusId == CVTemplateStatusId.PUBLISH && cc.CVTemplateId == t.Id
                    select t
                ).FirstOrDefault()
                select new CandidateCVAggregate
                {
                    Id = cc.Id,
                    Name = cc.Name,
                    CVTemplateId = cvTemplate != null ? cvTemplate.Id : null,
                    CVTemplateName = cvTemplate != null ? cvTemplate.Name : null,
                    CVTemplatePhoto = cvTemplate != null ? cvTemplate.Photo : null,
                    ModifiedTime = cc.ModifiedTime
                }
            ).ToListAsync();
        }
    }
}
