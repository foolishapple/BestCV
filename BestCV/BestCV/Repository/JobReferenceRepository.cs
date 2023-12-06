using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.JobRequireCity;
using Jobi.Domain.Aggregates.JobSuitable;
using Jobi.Domain.Aggregates.TopJobExtra;
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
    public class JobReferenceRepository : RepositoryBaseAsync<JobReference, long, JobiContext>, IJobReferenceRepository
    {
        private readonly JobiContext _context;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public JobReferenceRepository(JobiContext context, IUnitOfWork<JobiContext> unitOfWork) : base(context,unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
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
            return await _context.JobReferences.AnyAsync(c => c.JobId == id && c.Active);
        }

        /// <summary>
        /// Author : HoanNK
        /// Created: 19/09/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<bool> IsJobIdExistAsync(long jobId)
        {
            var existingJob = await _context.JobReferences
                .FirstOrDefaultAsync(js => js.JobId == jobId && js.Active);

            return existingJob != null;
        }

        /// <summary>
        /// Author : HoanNK
        /// Created: 19/09/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<List<JobReferenceAggregates>> ListAggregatesAsync()
        {
            return await(from js in _context.JobReferences
                         from j in _context.Jobs
                         where js.Active && j.Active && js.JobId == j.Id
                         select new JobReferenceAggregates()
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
            return await _context.Jobs.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        public async Task<List<JobReferenceAggregates>> ListJobReferenceOnDetailJob(long jobId)
        {
            return await (from te in _context.JobReferences
                          from j in _context.Jobs
                          from rc in _context.RecruitmentCampaigns
                          from com in _context.Companies
                          let servicebenefit = (from be in _context.Benefits
                                                from jsh in _context.JobServicePackages
                                                from spb in _context.ServicePackageBenefits
                                                from svp in _context.EmployerServicePackages
                                                where jsh.JobId == j.Id
                                                && jsh.Active
                                                && (jsh.CreatedTime.AddDays(jsh.ExpireTime ?? 0) > DateTime.Now)
                                                && be.Active
                                                && spb.Active
                                                && spb.BenefitId == be.Id
                                                && spb.EmployerServicePackageId == svp.Id
                                                && svp.Id == jsh.EmployerServicePackageId
                                                && be.Id == BenefitId.DE_XUAT_VIEC_LAM_LIEN_QUAN
                                                select be).Count() > 0


                          where te.Active && j.Active && te.JobId == j.Id && j.RecruimentCampaignId == rc.Id && rc.EmployerId == com.EmployerId && rc.Active && com.Active
                          //&& dayExpired > DateTime.Now && jobService.Active
                          && te.JobId == j.Id
                          && rc.Id == j.RecruimentCampaignId
                          && rc.IsAprroved
                          && servicebenefit
                          && j.Id != jobId
                          orderby Guid.NewGuid()
                          select new JobReferenceAggregates()
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
                              JobRequireCity = (from jrc in _context.JobRequireCities
                                                from job in _context.Jobs
                                                from wp in _context.WorkPlaces
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
                              ListBenefit = (from b in _context.Benefits
                                             from esp in _context.EmployerServicePackages
                                             from job in _context.Jobs
                                             from jesp in _context.JobServicePackages
                                             from spb in _context.ServicePackageBenefits
                                             let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                             where b.Active && esp.Active && job.Active && jesp.Active
                                             && job.Id == j.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                             select b).Distinct().ToList(),
                          }).Take(10).ToListAsync();
        }
    }
}
