using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.CVTemplate;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CVTemplateRepository : RepositoryBaseAsync<CVTemplate, long, JobiContext>, ICVTemplateRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public CVTemplateRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 22/08/2023
        /// Description: Lấy tất cả các template CV đã publish
        /// </summary>
        /// <returns>Danh sách tất cả template CV đã publish</returns>
        public async Task<List<CVTemplateListAggregate>> GetAllPublishAsync()
        {
            return await (
                from cvt in db.CVTemplates
                from cvts in db.CVTemplateStatuses

                where (
                    cvt.Active && cvts.Active &&
                    cvt.CVTemplateStatusId == cvts.Id &&
                    cvts.Id == CVTemplateStatusId.PUBLISH
                )

                orderby cvt.OrderSort

                select new CVTemplateListAggregate
                {
                    Id = cvt.Id,
                    Name = cvt.Name,
                    Description = cvt.Description,
                    Photo = cvt.Photo,
                    OrderSort = cvt.OrderSort,
                    Version = cvt.Version
                }
            ).ToListAsync();
        }
    }
}
