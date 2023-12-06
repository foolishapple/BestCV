using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Aggregates.JobRequireCity;
using Jobi.Domain.Aggregates.TopFeatureJob;
using Jobi.Domain.Aggregates.TopJobExtra;
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
    public class TopJobExtraRepository : RepositoryBaseAsync<TopJobExtra, long, JobiContext>, ITopJobExtraRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public TopJobExtraRepository(JobiContext _db, IUnitOfWork<JobiContext> _unitOfWork) : base(_db, _unitOfWork)
        {
            db =_db;
            unitOfWork = _unitOfWork;
        }

        public async Task ChangeOrderSort(List<TopJobExtra> objs)
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
            return await db.TopJobExtras.AnyAsync(c => c.OrderSort == orderSort && c.Id != Id);
        }

        public async Task<bool> IsJobIdExist(long id, long jobId)
        {
            return await db.TopJobExtras.AnyAsync(c => c.JobId == jobId && c.Id != id);
        }

        public async Task<List<TopJobExtraAggregates>> ListTopJobExtra()
        {
            return await (from row in db.TopJobExtras
                          join j in db.Jobs on row.JobId equals j.Id
                          where row.Active
                          && j.Active
                          
                          select new TopJobExtraAggregates
                          {
                              Id = row.Id,
                              JobId = row.JobId,
                              JobName = j.Name,
                              Active = row.Active,
                              CreatedTime = row.CreatedTime,
                              Description = row.Description,
                              OrderSort = row.OrderSort,
                              SubOrderSort = row.SubOrderSort,
                          }).OrderBy(s => s.OrderSort).ThenBy(s => s.SubOrderSort).ToListAsync();
        }

        public async Task<int> MaxOrderSort(int orderSort)
        {
            var maxSubSort = await db.TopJobExtras
                .Where(s => s.OrderSort == orderSort && s.Active)
                .Select(s => (int?)s.SubOrderSort)
                .MaxAsync() ?? -1;
            return maxSubSort;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get mex order sort
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaxOrderSort()
        {
            int maxOrderSort = await db.TopJobExtras.Where(c => c.Active).Select(c => c.OrderSort).MaxAsync();
            return maxOrderSort;
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
            return await db.TopJobExtras.AnyAsync(c => c.Active && c.JobId == id);
        }

        public async Task<List<TopJobExtraAggregates>> ListTopJobExtraShowOnHomePageAsync()
        {
            return await (from te in db.TopJobExtras
                          from j in db.Jobs
                          from rc in db.RecruitmentCampaigns
                          from com in db.Companies

                          let isDisplay = (from jsp in db.JobServicePackages
                                           join svp in db.EmployerServicePackages on jsp.EmployerServicePackageId equals svp.Id
                                           where jsp.Active
                                           && svp.Active
                                           && jsp.JobId == j.Id
                                           && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                           && (jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_PLUS_32_DAYS
                                                || jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_16_DAY)
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
                                                && be.Id == BenefitId.GAN_TAG_TOP
                                                select be).Count() > 0


                          where te.Active && j.Active && te.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active 
                          //&& dayExpired > DateTime.Now && jobService.Active
                          && te.JobId == j.Id
                          && rc.Id == j.RecruimentCampaignId
                          && rc.IsAprroved
                          && servicebenefit
                          && isDisplay
                          orderby Guid.NewGuid()
                          select new TopJobExtraAggregates()
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
                              OrderSort = te.OrderSort,
                              RecruimentCampaignId = j.RecruimentCampaignId,
                              Active = j.Active,
                              Id = te.Id,
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
                              SubOrderSort= te.SubOrderSort,
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


        public async Task<PagingData<List<TopJobExtraAggregates>>> SearchingFeatureJob(SearchJobWithServiceParameters parameter)
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


            var data = await (from te in db.TopJobExtras
                              from j in db.Jobs
                              from rc in db.RecruitmentCampaigns
                              from com in db.Companies

                              let isDisplay = (from jsp in db.JobServicePackages
                                               join svp in db.EmployerServicePackages on jsp.EmployerServicePackageId equals svp.Id
                                               where jsp.Active
                                               && svp.Active
                                               && jsp.JobId == j.Id
                                               && (jsp.CreatedTime.AddDays(jsp.ExpireTime ?? 0) > DateTime.Now)
                                               && (jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_PLUS_32_DAYS
                                                    || jsp.EmployerServicePackageId == ServicePackageConst.JOB_HOT_16_DAY)
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
                                                    && be.Id == BenefitId.GAN_TAG_TOP
                                                    select be).Count() > 0


                              where te.Active && j.Active && te.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                              //&& dayExpired > DateTime.Now && jobService.Active
                              && te.JobId == j.Id
                              && rc.Id == j.RecruimentCampaignId
                              && rc.IsAprroved
                              && servicebenefit
                              && isDisplay

                              orderby te.OrderSort , te.SubOrderSort ,te.CreatedTime
                              select new TopJobExtraAggregates()
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
                                  OrderSort = te.OrderSort,
                                  RecruimentCampaignId = j.RecruimentCampaignId,
                                  JobId = te.JobId,
                                  JobName = j.Name,
                                  Active = j.Active,
                                  Id = te.Id,
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

            return new PagingData<List<TopJobExtraAggregates>>
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
