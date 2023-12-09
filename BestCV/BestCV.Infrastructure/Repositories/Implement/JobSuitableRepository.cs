using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Aggregates.JobSuitable;
using BestCV.Domain.Aggregates.TopJobExtra;
using BestCV.Domain.Constants;
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
    public class JobSuitableRepository : RepositoryBaseAsync<JobSuitable, long, JobiContext>, IJobSuitableRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public JobSuitableRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<JobSuitableAggregates>> ListAggregatesAsync()
        {
            return await (from js in db.JobSuitables
                          from j in db.Jobs
                          where js.Active && j.Active && js.JobId == j.Id
                          select new JobSuitableAggregates()
                          {
                              Id = js.Id,
                              JobId = js.JobId,
                              JobName = j.Name,
                              Active = js.Active,
                              Description = js.Description,
                              CreatedTime = js.CreatedTime
                          }).ToListAsync();
        }


        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<bool> IsJobIdExistAsync(long jobId)
        {
            var existingJob = await db.JobSuitables
                .FirstOrDefaultAsync(js => js.JobId == jobId && js.Active);

            return existingJob != null;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
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


        public async Task<List<JobSuitableAggregates>> ListJobSuitableDashboard()
        {
            return await (from te in db.JobSuitables
                          from j in db.Jobs
                          from rc in db.RecruitmentCampaigns
                          from com in db.Companies

                          let servicebenefit = (from be in db.Benefits
                                                from jsh in db.JobServicePackages
                                                from spb in db.ServicePackageBenefits
                                                from svp in db.EmployerServicePackages
                                                where jsh.JobId == j.Id
                                                && jsh.Active
                                                && (jsh.CreatedTime.AddDays(jsh.ExpireTime ?? 0) > DateTime.Now)
                                                && be.Active
                                                && spb.Active
                                                && spb.BenefitId == be.Id
                                                && spb.EmployerServicePackageId == svp.Id
                                                && svp.Id == jsh.EmployerServicePackageId
                                                && be.Id == BenefitId.DE_XUAT_VIEC_LAM_PHU_HOP
                                                select be).Count() > 0


                          where te.Active && j.Active && te.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                          && te.JobId == j.Id
                          && rc.Id == j.RecruimentCampaignId
                          && rc.IsAprroved
                          && servicebenefit
                          orderby Guid.NewGuid()
                          select new JobSuitableAggregates()
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
                              ListBenefit = (from b in db.Benefits
                                             from esp in db.EmployerServicePackages
                                             from job in db.Jobs
                                             from jesp in db.JobServicePackages
                                             from spb in db.ServicePackageBenefits
                                             let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                             where b.Active && esp.Active && job.Active && jesp.Active
                                             && job.Id == j.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                             select b).Distinct().ToList(),
                          }).Take(5).ToListAsync();
        }


        public async Task<PagingData<List<JobSuitableAggregates>>> SearchingJobSuitable(SearchJobWithServiceParameters parameter)
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


            var data = await (from tj in db.JobSuitables
                              from j in db.Jobs
                              from rc in db.RecruitmentCampaigns
                              from com in db.Companies

                              let servicebenefit = (from be in db.Benefits
                                                    from jsh in db.JobServicePackages
                                                    from spb in db.ServicePackageBenefits
                                                    from svp in db.EmployerServicePackages
                                                    where jsh.JobId == j.Id
                                                    && jsh.Active
                                                    && (jsh.CreatedTime.AddDays(jsh.ExpireTime ?? 0) > DateTime.Now)
                                                    && be.Active
                                                    && spb.Active
                                                    && spb.BenefitId == be.Id
                                                    && spb.EmployerServicePackageId == svp.Id
                                                    && svp.Id == jsh.EmployerServicePackageId
                                                    && be.Id == BenefitId.DE_XUAT_VIEC_LAM_PHU_HOP
                                                    select be).Count() > 0

                              where tj.Active && j.Active && tj.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                                   && tj.JobId == j.Id
                              && rc.Id == j.RecruimentCampaignId
                              && rc.IsAprroved
                              && servicebenefit
                              orderby tj.CreatedTime
                              select new JobSuitableAggregates()
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

            return new PagingData<List<JobSuitableAggregates>>
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
