using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.InterviewSchedule;
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
    public class InterviewScheduleRepository : RepositoryBaseAsync<InterviewSchedule, int, JobiContext>, IInterviewScheduleRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public InterviewScheduleRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: HuyDQ
        /// Createad: 22/08/2023
        /// Description: Check interview Schedule name is existed
        /// </summary>
        /// <param name="name">interview Schedule name</param>
        /// <param name="id">interview Schedule id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.InterviewSchedules.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 22/08/2023
        /// Description: get list interview Schedule by candidateId
        /// </summary>
        /// <param name="candidateId">cadidate id</param>
        /// <returns></returns>
        public async Task<List<InterviewScheduleAggregates>> GetListInterViewsByCandidateId(long candidateId)
        {
            //Tạo danh sách cần lấy 
            return await (from li in _db.InterviewSchedules
                    join s in _db.InterviewStatuses on li.InterviewscheduleStatusId equals s.Id
                    join t in _db.InterviewTypes on li.InterviewscheduleTypeId equals t.Id
                    join c in _db.CandidateApplyJobs on li.CandidateApplyJobId equals c.Id
                    join cc in _db.CandidateCVPDFs on c.CandidateCVPDFId equals cc.Id 
                    where li.Active && s.Active && t.Active && c.Active && cc.Active && cc.CandidateId == candidateId
                    select new InterviewScheduleAggregates
                    {
                        Id = li.Id,
                        Name = li.Name,
                        InterviewscheduleTypeId = li.InterviewscheduleTypeId,
                        InterviewscheduleTypeName = t.Name,
                        InterviewscheduleStatusId = li.InterviewscheduleStatusId,
                        InterviewscheduleStatusName = s.Name,
                        Color = s.Color,
                        CandidateApplyJobId = c.Id,
                        Link = li.Link,
                        StartDate = li.StateDate,
                        EndDate = li.EndDate,
                        Location = li.Location,
                        Search = li.Search,
                        Description = li.Description,
                    }).ToListAsync();
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 29/08/2023
        /// Description: get list InterviewScheduleAggregates by employerId
        /// </summary>
        /// <param name="employerId">cadidate id</param>
        /// <returns></returns>
        public async Task<List<InterviewScheduleAggregates>> GetListInterViewsByEmployerId(long employerId)
        {
            //Tạo danh sách cần lấy 
            return await(from li in _db.InterviewSchedules
                         join s in _db.InterviewStatuses on li.InterviewscheduleStatusId equals s.Id
                         join t in _db.InterviewTypes on li.InterviewscheduleTypeId equals t.Id
                         join c in _db.CandidateApplyJobs on li.CandidateApplyJobId equals c.Id
                         join j in _db.Jobs on c.JobId equals j.Id
                         join r in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals r.Id
                         join e in _db.Employers on r.EmployerId equals e.Id
                         where li.Active && s.Active && t.Active && c.Active && j.Active && r.Active && e.Active && e.Id == employerId
                         select new InterviewScheduleAggregates
                         {
                             Id = li.Id,
                             Name = li.Name,
                             InterviewscheduleTypeId = li.InterviewscheduleTypeId,
                             InterviewscheduleTypeName = t.Name,
                             InterviewscheduleStatusId = li.InterviewscheduleStatusId,
                             InterviewscheduleStatusName = s.Name,
                             Color = s.Color,
                             CandidateApplyJobId = c.Id,
                             Link = li.Link,
                             StartDate = li.StateDate,
                             EndDate = li.EndDate,
                             Location = li.Location,
                             Search = li.Search,
                             Description = li.Description,
                         }).ToListAsync();
        }
    }
}
