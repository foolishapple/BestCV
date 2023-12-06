using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateApplyJobs;
using Jobi.Domain.Aggregates.CandidateSaveJob;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CandidateSaveJobRepository : RepositoryBaseAsync<CandidateSaveJob, long, JobiContext>, ICandidateSaveJobRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public CandidateSaveJobRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        public async Task<bool> IsJobIdExist(long accountId, long jobId)
        {
            return await db.CandidateSaveJobs.AnyAsync(s => s.JobId == jobId && s.CandidateId == accountId && s.Id != 0);
        }

        public async Task<CandidateSaveJob> GetByCandidateIdIdAndJobIdAsync(long candidateId, long jobId)
        {
            return await db.CandidateSaveJobs.FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.JobId == jobId && x.Active);
        }

        public async Task<List<CandidateSaveJobAggregates>> ListCandidateSaveJobByCandidateId(long candidateId)
        {
            var query = from csj in db.CandidateSaveJobs
                        join j in db.Jobs on csj.JobId equals j.Id
                        join jc in db.JobCategories on j.PrimaryJobCategoryId equals jc.Id
                        join jt in db.JobTypes on j.JobTypeId equals jt.Id
                        join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join co in db.Companies on rc.EmployerId equals co.EmployerId
                        where csj.CandidateId == candidateId
                            && csj.Active
                            && j.Active
                            && jc.Active
                            && jt.Active
                            && rc.Active
                            && co.Active
                        select new CandidateSaveJobAggregates()
                        {
                            Id = csj.Id,
                            CompanyId = co.Id,
                            CompanyName = co.Name,
                            JobCategoryId = jc.Id,
                            JobCategoryName = jc.Name,
                            JobId = j.Id,
                            JobName = j.Name,
                            JobTypeId = jt.Id,
                            JobTypeName = jt.Name,
                            CreatedTime = csj.CreatedTime
                        };
            return await query.ToListAsync();
        }

        public async Task<DTResult<CandidateSaveJobAggregates>> PagingByCandidateId(DTPagingCandidateSaveJobParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = from csj in db.CandidateSaveJobs
                        join j in db.Jobs on csj.JobId equals j.Id
                        join c in db.Candidates on csj.CandidateId equals c.Id
                        join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join em in db.Employers on rc.EmployerId equals em.Id
                        join com in db.Companies on em.Id equals com.EmployerId
                        where csj.Active && j.Active && c.Active && rc.Active && em.Active && com.Active && j.IsApproved && csj.CandidateId == parameters.CandidateId
                        orderby csj.CreatedTime descending
                        select new CandidateSaveJobAggregates()
                        {
                            Id = csj.Id,
                            CandidateId = csj.CandidateId,
                            CompanyId = com.Id,
                            CompanyName = com.Name,
                            CreatedTime = csj.CreatedTime,
                            JobId = csj.JobId,
                            JobName = j.Name,
                            CompanyAddress = com.AddressDetail,
                            CompanyPhone = com.Phone,
                            CompanyWebsite = com.Website,
                            CityRequired = (from wp in db.WorkPlaces
                                            join rqc in db.JobRequireCities on wp.Id equals rqc.CityId
                                            where wp.Active && rqc.Active && rqc.JobId == j.Id
                                            select wp.Name).ToList(),
                            SalaryFrom = j.SalaryFrom,
                            SalaryTo = j.SalaryTo,
                            SalaryTypeId = j.SalaryTypeId,
                            JobApplyEndDate = j.ApplyEndDate,
                        };

            int recordsTotal = await query.CountAsync();

            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => EF.Functions.Collate(c.JobName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || EF.Functions.Collate(c.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword)
                || ((from wp in db.WorkPlaces
                     join rqc in db.JobRequireCities on wp.Id equals rqc.CityId
                     where wp.Active && rqc.Active && rqc.JobId == c.JobId && EF.Functions.Collate(wp.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                     select wp.Name).Count() > 0));
            }

            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<CandidateSaveJobAggregates> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
    }
}
