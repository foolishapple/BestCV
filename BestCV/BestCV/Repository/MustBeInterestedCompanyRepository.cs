using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CompanyFieldOfActivity;
using Jobi.Domain.Aggregates.JobRequireCity;
using Jobi.Domain.Aggregates.JobSuitable;
using Jobi.Domain.Aggregates.MustBeInterestedCompany;
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
using System.Xml.Linq;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class MustBeInterestedCompanyRepository : RepositoryBaseAsync<MustBeInterestedCompany, long, JobiContext>, IMustBeInterestedCompanyRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public MustBeInterestedCompanyRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 11/09/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<bool> IsJobIdExistAsync(long jobId)
        {
            var existingJob = await db.MustBeInterestedCompanies
               .FirstOrDefaultAsync(js => js.CompanyId == jobId && js.Active);
            return existingJob != null;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 11/09/2023
        /// </summary>
        /// <returns></returns>
        public async Task<List<MustBeInterestedCompanyAggregates>> ListAggregatesAsync()
        {
            return await(from js in db.MustBeInterestedCompanies
                         from j in db.Companies
                         where js.Active && j.Active && js.CompanyId == j.Id
                         select new MustBeInterestedCompanyAggregates()
                         {
                             Id = js.Id,
                             CompanyId = js.CompanyId,
                             CompanyName = j.Name,
                             Active = js.Active,
                             Description = js.Description,
                             CreatedTime = js.CreatedTime
                         }).ToListAsync();
        }

        /// <summary>
        ///Author : HoanNK
        /// CreatedTime : 11/09/2023
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


        public async Task<List<MustBeInterestedCompanyAggregates>> ListCompanyInterestedOnDetailJob()
        {
            var data = from ci in db.MustBeInterestedCompanies
                       join c in db.Companies on ci.CompanyId equals c.Id
                       
                       let serviceBenefit = (from be in db.Benefits
                                             join spb in db.ServicePackageBenefits on be.Id equals spb.BenefitId
                                             join svp in db.EmployerServicePackages on spb.EmployerServicePackageId equals svp.Id
                                             join jsh in db.JobServicePackages on svp.Id equals jsh.EmployerServicePackageId
                                             join j in db.Jobs on jsh.JobId equals j.Id
                                             join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                                             join com in db.Companies on rc.EmployerId equals com.EmployerId
   
                                             where 
                                             (jsh.CreatedTime.AddDays(jsh.ExpireTime ?? 0) > DateTime.Now)
                                             && be.Active
                                             && spb.Active
                                             && spb.BenefitId == be.Id
                                             && jsh.Active
                                             && j.Active
                                             && rc.Active && rc.IsAprroved
                                             && com.Active && com.Id == c.Id
                                             select be).Count() > 0

                       where 
                       c.Active 
                       && ci.Active 
                       && serviceBenefit
                       orderby Guid.NewGuid()
                       select new MustBeInterestedCompanyAggregates
                       {
                           Id = ci.Id,
                           CompanyId = ci.CompanyId,
                           CompanyName = c.Name,
                           Active = ci.Active,
                           CompanyCoverPhoto = c.CoverPhoto,
                           CompanyLogo = c.Logo,
                           CountFollower = db.CandidateFollowCompanies.Count(x => x.Active && x.CompanyId == c.Id),
                           CountJob = (from job in db.Jobs
                                       join recument in db.RecruitmentCampaigns on job.RecruimentCampaignId equals recument.Id
                                       where job.Active 
                                       && recument.Active && recument.IsAprroved
                                       && recument.EmployerId == c.EmployerId
                                       select job).Count(),
                           CreatedTime = ci.CreatedTime,
                           Description = ci.Description,
                           CompanyFields = (from cf in db.CompanyFieldOfActivities
                                            from f in db.FieldOfActivities
                                            where cf.FieldOfActivityId == f.Id
                                            && cf.CompanyId == c.Id
                                            && cf.Active
                                            && f.Active
                                            && c.Active
                                            select new CompanyFieldOfActivityAggregates
                                            {
                                                Id = cf.Id,
                                                CompanyId = cf.CompanyId,
                                                Active = cf.Active,
                                                FieldOfActivityId = cf.FieldOfActivityId,
                                                CreatedTime = cf.CreatedTime,
                                                CompanyName = c.Name,
                                                FieldOfActivityName = f.Name
                                            }).ToList(),
                       };

            return await data.Take(4).ToListAsync();
        }
    }
}
