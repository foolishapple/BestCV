using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Skill;
using BestCV.Domain.Aggregates.TopFeatureJob;
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
    public class SkillRepository : RepositoryBaseAsync<Skill ,int, JobiContext>, ISkillRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public SkillRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
         
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: Nam Anh
        /// Created: 21/8/2023
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsNameExisAsync(string name, int id)
        {
            return await db.Skills.AnyAsync(c => c.Active && c.Name.ToLower() == name.ToLower().Trim() && c.Id != id);
        }

        public async Task<List<SkillAggregates>> searchSkills(Select2Aggregates select2Aggregates)
        {
            var searchStr = select2Aggregates.SearchString;
            var pageLimit = select2Aggregates.PageLimit;

            IQueryable<Skill> query;

            if (pageLimit != null)
            {
                query = (
                    from j in db.Skills
                    where (
                        j.Active &&
                        (string.IsNullOrEmpty(searchStr) || j.Name.Contains(searchStr))
                    )
                    orderby
                    j.Name ascending
                    select j
               ).Take((int)pageLimit);
            }
            else
            {
                query = (
                    from j in db.Skills
                        //join tj in db.TopFeatureJobs on j.Id equals tj.JobId
                    where (
                        j.Active &&
                        //tj.Active &&
                        (string.IsNullOrEmpty(searchStr) || j.Name.Contains(searchStr))
                    )
                    orderby j.Name ascending
                    select j
                );
            }

            var jobs = await query.ToListAsync();

            var result = jobs.Select(j => new SkillAggregates
            {
                // Giả sử bạn có thuộc tính Name trong TopFeatureJobAggregates để lưu trữ tên công việc
                Id = j.Id,
                Name = j.Name
                // Các thuộc tính khác có thể được gán giá trị mặc định hoặc bỏ trống nếu không cần thiết
            }).ToList();

            return result;
        }
    }
}
