using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Aggregates.JobRequireCity;
using Jobi.Domain.Aggregates.JobSuitable;
using Jobi.Domain.Aggregates.TopFeatureJob;
using Jobi.Domain.Aggregates.TopJobExtra;
using Jobi.Domain.Aggregates.TopJobManagement;
using Jobi.Domain.Constants;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class TopJobManagementRepository : RepositoryBaseAsync<TopJobManagement, long, JobiContext>, ITopJobManagementRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public TopJobManagementRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> IsJobIdExistAsync(long jobId)
        {
            var existingJob = await db.TopJobManagements
               .FirstOrDefaultAsync(js => js.JobId == jobId && js.Active);

            return existingJob != null;
        }

        public async Task<List<TopJobManagementAggregates>> ListAggregatesAsync()
        {
            return await(from js in db.TopJobManagements
                         from j in db.Jobs
                         where js.Active && j.Active && js.JobId == j.Id
                         select new TopJobManagementAggregates()
                         {
                             Id = js.Id,
                             JobId = js.JobId,
                             JobName = j.Name,
                             Active = js.Active,
                             Description = js.Description,
                             CreatedTime = js.CreatedTime
                         }).ToListAsync();
        }

        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await db.Jobs.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        public async Task<PagingData<List<TopJobManagementAggregates>>> SearchingManagementJob(SearchJobWithServiceParameters parameter)
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


            var data = await (from tj in db.TopJobManagements
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
                                                    && be.Id == BenefitId.GAN_TAG_TOP_MANAGEMENT
                                                    select be).Count() > 0

                              where tj.Active && j.Active && tj.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                                   && tj.JobId == j.Id
                              && rc.Id == j.RecruimentCampaignId
                              && rc.IsAprroved
                              && servicebenefit
                              orderby tj.CreatedTime
                              select new TopJobManagementAggregates()
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

            return new PagingData<List<TopJobManagementAggregates>>
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
