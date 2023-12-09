using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.RecruitmentCampaigns;
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
    public class RecruitmentCampaignRepository : RepositoryBaseAsync<RecruitmentCampaign, long, JobiContext>, IRecruitmentCampaignRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public RecruitmentCampaignRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/08/2023
        /// Description: list Recruitment Campaign aggregate datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = from rc in _db.RecruitmentCampaigns
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        join s in _db.RecruitmentCampaignStatuses on rc.RecruitmentCampaignStatusId equals s.Id
                        where rc.Active && e.Active && s.Active
                        orderby rc.CreatedTime descending
                        select new RecruitmentCampaignAggregate()
                        {
                            StatusColor = s.Color,
                            StatusName = s.Name,
                            Candidates = (from cj in _db.CandidateApplyJobs
                                          join j in _db.Jobs on cj.JobId equals j.Id
                                          join cvPDf in _db.CandidateCVPDFs on cj.CandidateCVPDFId equals cvPDf.Id
                                          join c in _db.Candidates on cvPDf.CandidateId equals c.Id
                                          join soure in _db.CandidateApplyJobSources on cj.CandidateApplyJobSourceId equals soure.Id
                                          where cj.Active && j.Active && j.RecruimentCampaignId == rc.Id && c.Active  && soure.Active
                                          select new RecruitmentCampaignCandidateAggregate()
                                          {
                                              Id = c.Id,
                                              Name = c.FullName,
                                              Photo = c.Photo
                                          }).Take(RecruitmentCampaignConst.MAXIMUM_CANDIDATE_SHOW).ToList(),
                            Description = rc.Description,
                            EmployerId = e.Id,
                            Id = rc.Id,
                            Jobs = (from j in _db.Jobs
                                    join js in _db.JobStatuses on j.JobStatusId equals js.Id
                                    where j.Active && js.Active && j.RecruimentCampaignId == rc.Id
                                    select new RecruitmentCampaignJobAggregate()
                                    {
                                        StatusColor = js.Color,
                                        StatusName = js.Name,
                                        Id = j.Id,
                                        Name = j.Name,
                                        IsApproved = j.IsApproved
                                    }).Take(RecruitmentCampaignConst.MAXIMUM_JOB_SHOW).ToList(),
                            Name = rc.Name,
                            TotalCandidate = (from cj in _db.CandidateApplyJobs
                                              join j in _db.Jobs on cj.JobId equals j.Id
                                              join cvPDf in _db.CandidateCVPDFs on cj.CandidateCVPDFId equals cvPDf.Id
                                              join c in _db.Candidates on cvPDf.CandidateId equals c.Id
                                              join soure in _db.CandidateApplyJobSources on cj.CandidateApplyJobSourceId equals soure.Id
                                              where cj.Active && j.Active && j.RecruimentCampaignId == rc.Id && c.Active  && soure.Active
                                              select cj.Id).Count(),
                            TotalJob = (from j in _db.Jobs
                                        join js in _db.JobStatuses on j.JobStatusId equals js.Id
                                        where j.Active && js.Active && j.RecruimentCampaignId == rc.Id
                                        select j.Id).Count(),
                            IsApproved = rc.IsAprroved,
                            RecruitmetnCampaginStatusId = s.Id
                        };
            if (parameters.EmployerId != null)
            {
                query = query.Where(c => c.EmployerId == parameters.EmployerId);
            }
            int recordsTotal = await query.CountAsync();
            //2.Fillter
            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c =>
                EF.Functions.Collate(c.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || (c.TotalCandidate>0 ? false : "chua co cv nao".Contains(noneVietnameseKeyword))
                || ("#"+c.Id.ToString()).Contains(noneVietnameseKeyword)
                || EF.Functions.Collate(c.StatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || ((from j in _db.Jobs
                   join js in _db.JobStatuses on j.JobStatusId equals js.Id
                   where j.Active && js.Active && j.RecruimentCampaignId == c.Id && 
                   (
                   EF.Functions.Collate(j.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || ("#" + j.Id.ToString()).Contains(noneVietnameseKeyword)
                || (j.IsApproved? EF.Functions.Collate(js.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)): "tin chua duoc duyet".Contains(noneVietnameseKeyword))
                   )
                   select j.Id).Count() > 0)
                || (((from j in _db.Jobs
                    join js in _db.JobStatuses on j.JobStatusId equals js.Id
                    where j.Active && js.Active && j.RecruimentCampaignId == c.Id
                    select j.Id).Count() > 0)?false:"chua co tin tuyen dung".Contains(noneVietnameseKeyword))
                );
            }
            if (parameters.ReccruitmentCampaginStatusIds.Count() > 0)
            {
                query = query.Where(c => parameters.ReccruitmentCampaginStatusIds.Contains(c.RecruitmetnCampaginStatusId));
            }
            if (parameters.IsApproveds.Count() > 0)
            {
                query = query.Where(c => parameters.IsApproveds.Contains(c.IsApproved));
            }
            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<RecruitmentCampaignAggregate> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: Check recruitment campaign name is exitsted
        /// </summary>
        /// <param name="name">recruitment campaign name</param>
        /// <param name="id">recruitment campagin id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, long id)
        {
            return await _db.RecruitmentCampaigns.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);
        }
    }
}
