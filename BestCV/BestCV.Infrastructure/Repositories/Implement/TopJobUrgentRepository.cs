using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Aggregates.TopJobExtra;
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

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class TopJobUrgentRepository : RepositoryBaseAsync<TopJobUrgent, long, JobiContext>, ITopJobUrgentRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public TopJobUrgentRepository(JobiContext _db, IUnitOfWork<JobiContext> _unitOfWork) : base(_db, _unitOfWork)
        {
            db = _db;
            unitOfWork = _unitOfWork;
        }

        public async Task ChangeOrderSort(List<TopJobUrgent> objs)
        {
            foreach (var obj in objs)
            {
                db.Attach(obj);
                db.Entry(obj).Property(c => c.OrderSort).IsModified = true;
                db.Entry(obj).Property(c => c.SubOrderSort).IsModified = true;
            }
        }

        public async Task<bool> CheckOrderSort(long Id, int orderSort)
        {
            return await db.TopJobUrgents.AnyAsync(c => c.OrderSort == orderSort && c.Id != Id);
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
            return await db.TopJobUrgents.AnyAsync(c => c.JobId == id && c.Active);
        }

        public async Task<bool> IsJobIdExist(long id, long jobId)
        {
            return await db.TopJobUrgents.AnyAsync(c => c.JobId == jobId && c.Id != id);
        }

        public async Task<List<TopJobUrgentAggregates>> ListTopJobUrgent()
        {
            return await (from row in db.TopJobUrgents
                          join j in db.Jobs on row.JobId equals j.Id
                          where row.Active
                          && j.Active
                          orderby row.OrderSort,row.SubOrderSort
                          select new TopJobUrgentAggregates
                          {
                              Id = row.Id,
                              JobId = row.JobId,
                              JobName = j.Name,
                              Active = row.Active,
                              CreatedTime = row.CreatedTime,
                              Description = row.Description,
                              OrderSort = row.OrderSort,
                              SubOrderSort = row.SubOrderSort,
                          }).ToListAsync();
        }

        public async Task<int> MaxOrderSort(int orderSort)
        {
            var maxSubSort = await db.TopJobUrgents
               .Where(s => s.OrderSort == orderSort && s.Active)
               .Select(s => (int?)s.SubOrderSort)
               .MaxAsync() ?? -1;
            return maxSubSort;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get max order sort
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaxOrderSort()
        {
            int maxOrrderSort = await db.TopJobUrgents.Where(c => c.Active).Select(c => c.OrderSort).MaxAsync();
            return maxOrrderSort;
        }

        public async Task<List<TopJobUrgentAggregates>> ListTopJobUrgentShowOnHomePageAsync()
        {
            return await (from tj in db.TopJobUrgents
                          join j in db.Jobs on tj.JobId equals j.Id
                          join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                          join e in db.Employers on rc.EmployerId equals e.Id
                          join com in db.Companies on e.Id equals com.EmployerId

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
                          //let isNew = j.CreatedTime.AddDays(1) > DateTime.Now
                          let servicebenefit = (from be in db.Benefits
                                                join spb in db.ServicePackageBenefits on be.Id equals spb.BenefitId
                                                join svp in db.EmployerServicePackages on spb.EmployerServicePackageId equals svp.Id
                                                join jsp in db.JobServicePackages on svp.Id equals jsp.EmployerServicePackageId
                                                where
                                                be.Active
                                                && spb.Active
                                                && jsp.JobId == j.Id
                                                && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                                && be.Id == BenefitId.GAN_TAG_URGENT
                                                select be).Count() > 0


                          where tj.Active
                          && j.Active
                          && rc.Active
                          && com.Active
                          && rc.IsAprroved
                          && servicebenefit
                          && isDisplay
                          orderby Guid.NewGuid()
                          select new TopJobUrgentAggregates()
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
                              Description = j.Description,
                              JobId = j.Id,
                              JobName = j.Name,
                              SubOrderSort = tj.SubOrderSort,
                              ListBenefit = (from b in db.Benefits
                                             from esp in db.EmployerServicePackages
                                             from job in db.Jobs
                                             from jesp in db.JobServicePackages
                                             from spb in db.ServicePackageBenefits
                                             let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                             where b.Active && esp.Active && job.Active && jesp.Active
                                             && job.Id == j.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                             select b).Distinct().ToList(),
                          }).Take(JobConst.PAGE_SIZE_JOB_HOME_PAGE).ToListAsync();
        }


        public async Task<PagingData<List<TopJobUrgentAggregates>>> SearchingUrgentJob(SearchJobWithServiceParameters parameter)
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


            var data = await (from tj in db.TopJobUrgents
                              join j in db.Jobs on tj.JobId equals j.Id
                              join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                              join e in db.Employers on rc.EmployerId equals e.Id
                              join com in db.Companies on e.Id equals com.EmployerId

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
                              //let isNew = j.CreatedTime.AddDays(1) > DateTime.Now
                              let servicebenefit = (from be in db.Benefits
                                                    join spb in db.ServicePackageBenefits on be.Id equals spb.BenefitId
                                                    join svp in db.EmployerServicePackages on spb.EmployerServicePackageId equals svp.Id
                                                    join jsp in db.JobServicePackages on svp.Id equals jsp.EmployerServicePackageId
                                                    where
                                                    be.Active
                                                    && spb.Active
                                                    && jsp.JobId == j.Id
                                                    && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                                    && be.Id == BenefitId.GAN_TAG_URGENT
                                                    select be).Count() > 0


                              where tj.Active
                              && j.Active
                              && rc.Active
                              && com.Active
                              && rc.IsAprroved
                              && servicebenefit
                              && isDisplay
                              orderby tj.OrderSort, tj.SubOrderSort, tj.CreatedTime
                              select new TopJobUrgentAggregates()
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
                                  JobId = tj.JobId,
                                  JobName = j.Name,
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

                                  IsSaveJob = db.CandidateSaveJobs.Any(x => x.Active && x.CandidateId == parameter.CandidateId && x.JobId == j.Id)

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

            return new PagingData<List<TopJobUrgentAggregates>>
            {
                DataSource = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }
    }
}
