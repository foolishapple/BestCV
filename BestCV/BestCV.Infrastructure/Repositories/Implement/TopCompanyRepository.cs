using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.TopCompany;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class TopCompanyRepository : RepositoryBaseAsync<TopCompany, int, JobiContext>, ITopCompanyRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public TopCompanyRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 14/8/2023
        /// Update : Thanh
        /// UpdatedTime : 14/08/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TopCompanyAggregates>> ListTopCompanyShowOnHomePageAsync()
        {
            return await (from tc in db.TopCompanies
                          from c in db.Companies
                          where tc.Active && c.Active && tc.CompanyId == c.Id
                          orderby tc.OrderSort
                          select new TopCompanyAggregates()
                          {
                              Id = tc.Id,
                              Active = tc.Active,
                              OrderSort = tc.OrderSort,
                              TopCompanyId = c.Id,
                              TopCompanyName = c.Name,
                              CreatedTime = c.CreatedTime,
                              EmployerId = c.EmployerId,
                              CompanySizeId = c.CompanySizeId,
                              AddressDetail = c.AddressDetail,
                              Location = c.Location,
                              Website = c.Website,
                              Phone = c.Phone,
                              Logo = c.Logo,
                              CoverPhoto = c.CoverPhoto,
                              TaxCode = c.TaxCode,
                              EmailAddress = c.EmailAddress,
                              Description = c.Description,
                              Name = c.Name,
                              CountJob = (from row in db.Jobs
                                          from jc in db.JobCategories
                                          from js in db.JobStatuses
                                          from jt in db.JobTypes
                                          from jp in db.JobPosition
                                          from er in db.ExperienceRange
                                          from st in db.SalaryType
                                          from e in db.Employers
                                          from com in db.Companies
                                          from rc in db.RecruitmentCampaigns
                                          where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && com.Active && rc.Active
                                          && row.PrimaryJobCategoryId == jc.Id
                                          && row.JobStatusId == js.Id
                                          && row.JobTypeId == jt.Id
                                          && row.JobPositionId == jp.Id
                                          && row.ExperienceRangeId == er.Id
                                          && row.SalaryTypeId == st.Id
                                          && row.RecruimentCampaignId == rc.Id
                                          && com.EmployerId == rc.EmployerId
                                          && rc.EmployerId == e.Id
                                          && row.IsApproved
                                          && rc.IsAprroved && e.Id == c.EmployerId
                                          select row).Count()
                          }).Take(8).ToListAsync();
        }
        /// <summary>
        /// Author: Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task ChangeOrderSort(List<TopCompany> objs)
        {
            foreach (var obj in objs)
            {
                db.Attach(obj);
                db.Entry(obj).Property(c => c.OrderSort).IsModified = true;
                db.Entry(obj).Property(c => c.SubOrderSort).IsModified = true;
            }
        }
        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<TopCompany> FindByCompanyIdAsync(int companyId)
        {
            return await db.TopCompanies
                .Where(item => item.CompanyId == companyId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="orderSort"></param>
        /// <returns></returns>
        public async Task<TopCompany> FindByOrderSortAsync(int orderSort)
        {
            return await db.TopCompanies
                .Where(item => item.OrderSort == orderSort)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 14/8/2023
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectListItem>> ListCompanySelected()
        {
            return await db.Companies.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 14/8/2023
        /// </summary>
        /// <returns></returns>
        public async Task<List<TopCompany>> FindByConditionAsync(Expression<Func<TopCompany, bool>> expression)
        {
            return await db.TopCompanies
                        .Include(tc => tc.Company)
                        .Where(expression)
                        .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 15/8/2023
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<TopCompany> GetByCompanyIdAsync(int companyId)
        {
            return await db.TopCompanies.FirstOrDefaultAsync(tc => tc.CompanyId == companyId);
        }
        public async Task<List<TopCompanyAggregates>> ListTopCompany()
        {
            return await (from row in db.TopCompanies
                          join j in db.Companies on row.CompanyId equals j.Id
                          where row.Active
                          && j.Active
                          orderby row.OrderSort, row.SubOrderSort
                          select new TopCompanyAggregates
                          {
                              Id = row.Id,
                              TopCompanyId = row.CompanyId,
                              TopCompanyName = j.Name,
                              Active = row.Active,
                              CreatedTime = row.CreatedTime,
                              Description = j.Description,
                              OrderSort = row.OrderSort,
                              SubOrderSort = row.SubOrderSort,
                          }).ToListAsync();
        }
        /// <summary>
        /// Author: Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="orderSort"></param>
        /// <returns></returns>
        public async Task<int> MaxOrderSort(int orderSort)
        {
            var maxSubSort = await db.TopCompanies
                .Where(s => s.OrderSort == orderSort && s.Active)
                .Select(s => (int?)s.SubOrderSort)
                .MaxAsync() ?? -1;
            return maxSubSort;
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<bool> IsCompanyIdExist(long id, long companyId)
        {
            return await db.TopCompanies.AnyAsync(c => c.CompanyId == companyId && c.Active && c.Id != id);
        }
        /// <summary>
        /// Author:Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="orderSort"></param>
        /// <returns></returns>
        public async Task<bool> CheckOrderSort(long Id, int orderSort)
        {
            return await db.TopCompanies.AnyAsync(c => c.OrderSort == orderSort && c.Id != Id);
        }
    }
}
