using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.JobRequireCity;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.TopCompany;
using Jobi.Domain.Aggregates.TopFeatureJob;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jobi.Domain.Constants;
using Jobi.Domain.Aggregates.Job;
using System.Reflection.Metadata;
using Jobi.Core.Entities;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class TopFeatureJobRepository : RepositoryBaseAsync<TopFeatureJob, int, JobiContext>, ITopFeatureJobRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public TopFeatureJobRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TopFeatureJob> GetByJobIdAsync(long jobId)
        {
            return await db.TopFeatureJobs.FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await db.Jobs.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TopFeatureJob>> FindByConditionAsync(Expression<Func<TopFeatureJob, bool>> expression)
        {
            return await db.TopFeatureJobs
                        .Include(tc => tc.Job)
                        .Where(expression)
                        .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 18/8/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TopFeatureJob> GetByIdAsync(int id)
        {
            var topFeatureJob = await db.TopFeatureJobs
                .Include(tfj => tfj.Job)
                .FirstOrDefaultAsync(tfj => tfj.Id == id);

            return topFeatureJob;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        async public Task<List<TopFeatureJobAggregates>> searchJobs(Select2Aggregates select2Aggregates)
        {
            var searchStr = select2Aggregates.SearchString;
            var pageLimit = select2Aggregates.PageLimit;

            IQueryable<Job> query;

            if (pageLimit != null)
            {
                query = (
                    from j in db.Jobs
                        //join tj in db.TopFeatureJobs on j.Id equals tj.JobId
                    where (
                        j.Active &&
                        //tj.Active &&
                        (string.IsNullOrEmpty(searchStr) || j.Name.Contains(searchStr))
                    )
                    orderby
                    //tj.OrderSort ascending,
                    j.Name ascending
                    select j
                ).Include(tj => tj.TopFeatureJobs).Take((int)pageLimit);
            }
            else
            {
                query = (
                    from j in db.Jobs
                        //join tj in db.TopFeatureJobs on j.Id equals tj.JobId
                    where (
                        j.Active &&
                        //tj.Active &&
                        (string.IsNullOrEmpty(searchStr) || j.Name.Contains(searchStr))
                    )
                    orderby j.Name ascending
                    select j
                );
                //.Include(tj => tj.TopFeatureJobs); // Thay đổi ở đây
            }

            var jobs = await query.ToListAsync();

            var result = jobs.Select(j => new TopFeatureJobAggregates
            {
                // Giả sử bạn có thuộc tính Name trong TopFeatureJobAggregates để lưu trữ tên công việc
                TopFeatureJobId = j.Id,
                TopFeatureJobName = j.Name
                // Các thuộc tính khác có thể được gán giá trị mặc định hoặc bỏ trống nếu không cần thiết
            }).ToList();

            return result;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<TopFeatureJob> FindByJobIdAsync(long jobId)
        {
            return await db.TopFeatureJobs
                .Where(item => item.JobId == jobId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="orderSort"></param>
        /// <returns></returns>
        public async Task<TopFeatureJob> FindByOrderSortAsync(int orderSort)
        {
            return await db.TopFeatureJobs
                .Where(item => item.OrderSort == orderSort)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 14/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TopFeatureJobAggregates>> ListTopFeatureJobShowOnHomePageAsync()
        {
            return await (from tj in db.TopFeatureJobs
                          from j in db.Jobs
                          from rc in db.RecruitmentCampaigns
                          from com in db.Companies
                          let isNew = j.RefreshDate > DateTime.Now.AddDays(-1)

                          let isDisplay = (from jsp in db.JobServicePackages
                                           join svp in db.EmployerServicePackages on jsp.EmployerServicePackageId equals svp.Id
                                           where jsp.Active
                                           && svp.Active
                                           && jsp.JobId == j.Id
                                           && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                           && (jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_PLUS_32_DAYS
                                                || jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_16_DAY
                                                || jsp.EmployerServicePackageId == ServicePackageConst.JOB_TOP_PRIORITY_PLUS_32_DAYS
                                                || jsp.EmployerServicePackageId == ServicePackageConst.JOB_TOP_PRIORITY_16_DAYS)
                                           select jsp).Any()

                          let servicebenefit = (from be in db.Benefits
                                                join spb in db.ServicePackageBenefits on be.Id equals spb.BenefitId
                                                join svp in db.EmployerServicePackages on spb.EmployerServicePackageId equals svp.Id
                                                join jsp in db.JobServicePackages on svp.Id equals jsp.EmployerServicePackageId
                                                where
                                                be.Active
                                                && spb.Active
                                                && jsp.JobId == j.Id
                                                && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                                && be.Id == BenefitId.GAN_TAG_NEW
                                                select be).Count() > 0


                          where tj.Active && j.Active && tj.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                               && tj.JobId == j.Id
                          && rc.Id == j.RecruimentCampaignId
                          && rc.IsAprroved
                          && servicebenefit
                          && isNew
                          && isDisplay
                          orderby Guid.NewGuid()
                          select new TopFeatureJobAggregates()
                          {
                              ExperienceRangeId = j.ExperienceRangeId,
                              JobStatusId = j.JobStatusId,
                              PrimaryJobCategoryId = j.PrimaryJobCategoryId,
                              TotalRecruitment = j.TotalRecruitment,
                              GenderRequirement = j.GenderRequirement,
                              JobTypeId = j.JobTypeId,
                              JobPositionId = j.JobPositionId,
                              IsApproved = j.IsApproved,
                              Currency = j.Currency,
                              SalaryTypeId = j.SalaryTypeId,
                              SalaryFrom = j.SalaryFrom,
                              SalaryTo = j.SalaryTo,
                              Overview = j.Overview,
                              Requirement = j.Requirement,
                              Benefit = j.Benefit,
                              ReceiverName = j.ReceiverName,
                              ReceiverEmail = j.ReceiverEmail,
                              ReceiverPhone = j.ReceiverPhone,
                              ApplyEndDate = j.ApplyEndDate,
                              ApprovalDate = j.ApprovalDate,
                              OrderSort = tj.OrderSort,
                              RecruimentCampaignId = j.RecruimentCampaignId,
                              TopFeatureJobId = tj.JobId,
                              TopFeatureJobName = j.Name,
                              Active = j.Active,
                              Id = tj.Id,
                              JobRequireCity = (from jrc in db.JobRequireCities
                                                from job in db.Jobs
                                                from wp in db.WorkPlaces
                                                where jrc.Active && job.Active && wp.Active && jrc.JobId == job.Id && jrc.JobId == j.Id && jrc.CityId == wp.Id
                                                select new JobRequireCityAggregates
                                                {
                                                    Id = jrc.Id,
                                                    CityId = jrc.CityId,
                                                    JobId = jrc.Id,
                                                    Active = jrc.Active,
                                                    CityName = wp.Name,
                                                    CreatedTime = jrc.CreatedTime,
                                                    JobName = job.Name
                                                }).ToList(),
                              CompanyId = com.Id,
                              CompanyName = com.Name,
                              CompanyLogo = com.Logo,
                              JobCreatedTime = j.CreatedTime,
                              JobTypeName = j.JobType.Name,
                              PrimaryJobCategoryName = j.PrimaryJobCategory.Name,
                              PrimaryJobCategoryIcon = j.PrimaryJobCategory.Icon,
                              ListBenefit = (from b in db.Benefits
                                             from esp in db.EmployerServicePackages
                                             from job in db.Jobs
                                             from jesp in db.JobServicePackages
                                             from spb in db.ServicePackageBenefits
                                             let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                             where b.Active && esp.Active && job.Active && jesp.Active
                                             && job.Id == j.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                             select b).Distinct().ToList(),
                              CreatedTime = j.CreatedTime,
                              RefreshDate = j.RefreshDate
                          }).Take(JobConst.PAGE_SIZE_JOB_HOME_PAGE).ToListAsync();
        }

        public async Task<PagingData<List<TopFeatureJobAggregates>>> SearchingFeatureJob(SearchJobWithServiceParameters parameter)
        {
            //khai báo biến
            var pageIndex = parameter.PageIndex;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var pageSize = parameter.PageSize;

            if (parameter.OrderCriteria != null)
            {
                orderCriteria = parameter.OrderCriteria;
                orderAscendingDirection = parameter.OrderCriteria.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }


            var data = await (from tj in db.TopFeatureJobs
                              from j in db.Jobs
                              from rc in db.RecruitmentCampaigns
                              from com in db.Companies
                              let isNew = j.RefreshDate > DateTime.Now.AddDays(-1)

                              let isDisplay = (from jsp in db.JobServicePackages
                                               join svp in db.EmployerServicePackages on jsp.EmployerServicePackageId equals svp.Id
                                               where jsp.Active
                                               && svp.Active
                                               && jsp.JobId == j.Id
                                               && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                               && (jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_PLUS_32_DAYS
                                                    || jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_16_DAY
                                                    || jsp.EmployerServicePackageId == ServicePackageConst.JOB_TOP_PRIORITY_PLUS_32_DAYS
                                                    || jsp.EmployerServicePackageId == ServicePackageConst.JOB_TOP_PRIORITY_16_DAYS)
                                               select jsp).Any()

                              let servicebenefit = (from be in db.Benefits
                                                    join spb in db.ServicePackageBenefits on be.Id equals spb.BenefitId
                                                    join svp in db.EmployerServicePackages on spb.EmployerServicePackageId equals svp.Id
                                                    join jsp in db.JobServicePackages on svp.Id equals jsp.EmployerServicePackageId
                                                    where
                                                    be.Active
                                                    && spb.Active
                                                    && jsp.JobId == j.Id
                                                    && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                                    && be.Id == BenefitId.GAN_TAG_NEW
                                                    select be).Count() > 0


                              where tj.Active && j.Active && tj.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                                   && tj.JobId == j.Id
                              && rc.Id == j.RecruimentCampaignId
                              && rc.IsAprroved
                              && servicebenefit
                              && isNew
                              && isDisplay
                              orderby tj.OrderSort, tj.SubOrderSort, tj.CreatedTime
                              select new TopFeatureJobAggregates()
                              {
                                  ExperienceRangeId = j.ExperienceRangeId,
                                  JobStatusId = j.JobStatusId,
                                  PrimaryJobCategoryId = j.PrimaryJobCategoryId,
                                  TotalRecruitment = j.TotalRecruitment,
                                  GenderRequirement = j.GenderRequirement,
                                  JobTypeId = j.JobTypeId,
                                  JobPositionId = j.JobPositionId,
                                  IsApproved = j.IsApproved,
                                  Currency = j.Currency,
                                  SalaryTypeId = j.SalaryTypeId,
                                  SalaryFrom = j.SalaryFrom,
                                  SalaryTo = j.SalaryTo,
                                  Overview = j.Overview,
                                  Requirement = j.Requirement,
                                  Benefit = j.Benefit,
                                  ReceiverName = j.ReceiverName,
                                  ReceiverEmail = j.ReceiverEmail,
                                  ReceiverPhone = j.ReceiverPhone,
                                  ApplyEndDate = j.ApplyEndDate,
                                  ApprovalDate = j.ApprovalDate,
                                  OrderSort = tj.OrderSort,
                                  RecruimentCampaignId = j.RecruimentCampaignId,
                                  TopFeatureJobId = tj.JobId,
                                  TopFeatureJobName = j.Name,
                                  Active = j.Active,
                                  Id = tj.Id,
                                  JobRequireCity = (from jrc in db.JobRequireCities
                                                    from job in db.Jobs
                                                    from wp in db.WorkPlaces
                                                    where jrc.Active && job.Active && wp.Active && jrc.JobId == job.Id && jrc.JobId == j.Id && jrc.CityId == wp.Id
                                                    select new JobRequireCityAggregates
                                                    {
                                                        Id = jrc.Id,
                                                        CityId = jrc.CityId,
                                                        JobId = jrc.Id,
                                                        Active = jrc.Active,
                                                        CityName = wp.Name,
                                                        CreatedTime = jrc.CreatedTime,
                                                        JobName = job.Name
                                                    }).ToList(),
                                  CompanyId = com.Id,
                                  CompanyName = com.Name,
                                  CompanyLogo = com.Logo,
                                  JobCreatedTime = j.CreatedTime,
                                  JobTypeName = j.JobType.Name,
                                  PrimaryJobCategoryName = j.PrimaryJobCategory.Name,
                                  PrimaryJobCategoryIcon = j.PrimaryJobCategory.Icon,
                                  ListBenefit = (from b in db.Benefits
                                                 from esp in db.EmployerServicePackages
                                                 from job in db.Jobs
                                                 from jesp in db.JobServicePackages
                                                 from spb in db.ServicePackageBenefits
                                                 let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                                 where b.Active && esp.Active && job.Active && jesp.Active
                                                 && job.Id == j.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                                 select b).Distinct().ToList(),

                                  IsSaveJob = db.CandidateSaveJobs.Any(x => x.Active && x.CandidateId == parameter.CandidateId && x.JobId == j.Id),
                                  RefreshDate = j.RefreshDate

                              }).ToListAsync();

            var allRecord = data.Count;


            //orderby
            if (parameter.OrderCriteria != null)
            {
                switch (parameter.OrderCriteria)
                {
                    case "asc":
                        data = data.OrderBy(x => x.Id).ToList();
                        break;
                    case "desc":
                        data = data.OrderByDescending(x => x.CreatedTime).ToList();
                        break;

                }
            }
            var recordsTotal = data.Count;

            return new PagingData<List<TopFeatureJobAggregates>>
            {
                DataSource = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Check job is existed 
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        public async Task<bool> IsExisted(long id)
        {
            return await db.TopFeatureJobs.AnyAsync(c => c.JobId == id && c.Active);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get max order sort
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaxOrderSort()
        {
            int maxOrrderSort = await db.TopFeatureJobs.Where(c => c.Active).Select(c => c.OrderSort).MaxAsync();
            return maxOrrderSort;
        }
        /// <summary>
        /// Author: Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task ChangeOrderSort(List<TopFeatureJob> objs)
        {
            foreach (var obj in objs)
            {
                db.Attach(obj);
                db.Entry(obj).Property(c => c.OrderSort).IsModified = true;
                db.Entry(obj).Property(c => c.SubOrderSort).IsModified = true;
            }
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 10/10/2023
        /// </summary>
        /// <returns></returns>
        public async Task<List<TopFeatureJobAggregates>> ListTopFeatureJob()
        {
            return await (from row in db.TopFeatureJobs
                          join j in db.Jobs on row.JobId equals j.Id
                          where row.Active
                          && j.Active
                          orderby row.OrderSort, row.SubOrderSort
                          select new TopFeatureJobAggregates
                          {
                              Id = row.Id,
                              TopFeatureJobId = row.JobId,
                              TopFeatureJobName = j.Name,
                              Active = row.Active,
                              CreatedTime = row.CreatedTime,
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
            var maxSubSort = await db.TopFeatureJobs
                .Where(s => s.OrderSort == orderSort && s.Active)
                .Select(s => (int?)s.SubOrderSort)
                .MaxAsync() ?? -1;
            return maxSubSort;
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
            return await db.TopFeatureJobs.AnyAsync(c => c.OrderSort == orderSort && c.Id != Id);
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<bool> IsFeatureIdExist(long id, long jobId)
        {
            return await db.TopFeatureJobs.AnyAsync(c => c.JobId == jobId && c.Active && c.Id != id);
        }
    }
}
