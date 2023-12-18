using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateApplyJobRepository : RepositoryBaseAsync<CandidateApplyJob,long,JobiContext>,ICandidateApplyJobRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public CandidateApplyJobRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
       
        public async Task<int> CountByCondition(CountCandidateApplyJobCondition condition)
        {
            var query = from caj in _db.CandidateApplyJobs
                        join cajs in _db.CandidateApplyJobSources on caj.CandidateApplyJobSourceId equals cajs.Id
                        join j in _db.Jobs on caj.JobId equals j.Id
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        where caj.Active && cajs.Active && j.Active && rc.Active
                        select new { caj, cajs, j, rc };
            if (condition.CandidateApplyJobSourceIds.Count > 0)
            {
                query = query.Where(c => condition.CandidateApplyJobSourceIds.Contains(c.cajs.Id));
            }
            if (condition.JobIds.Count > 0)
            {
                query = query.Where(c => condition.JobIds.Contains(c.j.Id));
            }
            if (condition.RecruitmentCampaginIds.Count > 0)
            {
                query = query.Where(c => condition.RecruitmentCampaginIds.Contains(c.rc.Id));
            }
            int count = await query.CountAsync();
            return count;
        }

       
        public async Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = from caj in _db.CandidateApplyJobs
                        join j in _db.Jobs on caj.JobId equals j.Id
                        join rcmc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rcmc.Id
                        join e in _db.Employers on rcmc.EmployerId equals e.Id
                        join s in _db.CandidateApplyJobSources on caj.CandidateApplyJobSourceId equals s.Id
                        join cvPDf in _db.CandidateCVPDFs on caj.CandidateCVPDFId equals cvPDf.Id
                        join c in _db.Candidates on cvPDf.CandidateId equals c.Id
                        join cs in _db.CandidateApplyJobStatuses on caj.CandidateApplyJobStatusId equals cs.Id
                        where caj.Active && j.Active && rcmc.Active && e.Active && s.Active && c.Active && cs.Active  
                        orderby caj.CreatedTime descending
                        select new CandidateApplyJobAggregate()
                        {
                            Id = caj.Id,
                            EmployerId = e.Id,
                            CandidateApplyJobSourceName = s.Name,
                            CandidateAvatar = c.Photo,
                            CandidateEmail = c.Email,
                            CandidateName = c.FullName,
                            CandidatePhone = c.Phone,
                            CreatedTime = caj.CreatedTime,
                            IsEmployerViewed = caj.IsEmployerViewed,
                            RecruimentCampaignId = rcmc.Id,
                            RecruimentCampaignName = rcmc.Name,
                            CandidateApplyJobStatusColor = cs.Color,
                            CandidateApplyJobStatusName = cs.Name,
                            Description = caj.Description,
                            JobName = j.Name,
                            CandidateApplyJobStatusId = cs.Id,
                            CandidateApplyJobSourceId = s.Id,
                            CandidateCVPDFUrl = cvPDf.Url,
                            JobId = j.Id
                        };
            if (parameters.IsViewUnread)
            {
                query = query.Where(c => c.IsEmployerViewed == false);
            }
            if (parameters.EmployerId != null)
            {
                query = query.Where(c => c.EmployerId == parameters.EmployerId);
            }
            if (parameters.RecruitmentCampaignId != null)
            {
                query = query.Where(c => c.RecruimentCampaignId == parameters.RecruitmentCampaignId);
            }
            if (parameters.CandidateApplyJobSourceId != null)
            {
                query = query.Where(c => c.CandidateApplyJobSourceId == parameters.CandidateApplyJobSourceId);
            }
            if (parameters.JobId != null)
            {
                query = query.Where(c => c.JobId == parameters.JobId);
            }
            int recordsTotal = await query.CountAsync();
            //2.Fillter
            if (parameters.RecruitmentCampaignIds.Count() > 0)
            {
                query = query.Where(c => parameters.RecruitmentCampaignIds.Contains(c.RecruimentCampaignId));
            }
            if (parameters.CandidateApplyJobStatusIds.Count() > 0)
            {
                query = query.Where(c => parameters.CandidateApplyJobStatusIds.Contains(c.CandidateApplyJobStatusId));
            }
            if (parameters.CandidateApplyJobSourceIds.Count() > 0)
            {
                query = query.Where(c => parameters.CandidateApplyJobSourceIds.Contains(c.CandidateApplyJobSourceId));
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c =>
                EF.Functions.Collate(c.CandidateName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || (c.IsEmployerViewed?"da doc".Contains(noneVietnameseKeyword): "chua doc".Contains(noneVietnameseKeyword))
                || EF.Functions.Collate(c.RecruimentCampaignName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || ("#"+ c.RecruimentCampaignId.ToString()).Contains(noneVietnameseKeyword)
                || c.CandidateEmail.Contains(noneVietnameseKeyword)
                || c.CandidatePhone.Contains(noneVietnameseKeyword)
                || EF.Functions.Collate(c.CandidateApplyJobSourceName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(c.JobName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword)
                || EF.Functions.Collate(c.CandidateApplyJobStatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                );
            }
            foreach(var item in parameters.Columns)
            {
                string search = item.Search.Value.Trim().ToLower();
                switch (item.Data)
                {
                    case "title":
                        query = query.Where(c => c.CandidateName.ToLower().Contains(search) || (c.IsEmployerViewed ? "đã đọc" : "chưa đọc").Contains(search));
                        break;
                    case "contact":
                        query = query.Where(c => c.CandidateEmail.ToLower().Contains(search) || c.CandidatePhone.ToLower().Contains(search));
                        break;
                    case "other":
                        query = query.Where(c => c.CandidateApplyJobSourceName.ToLower().Contains(search) || c.CreatedTime.ToCustomString().Contains(search) || c.JobName.ToLower().Contains(search));
                        break;
                }
            }
            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<CandidateApplyJobAggregate> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }

        public async Task<bool> IsJobIdExist(long accountId, long jobId)
        {
            return await _db.CandidateApplyJobs.AnyAsync(s => s.JobId == jobId && s.CandidateCVPDFId == accountId && s.Id != 0);
        }

        public async Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = from caj in _db.CandidateApplyJobs
                        join j in _db.Jobs on caj.JobId equals j.Id
                        join rcmc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rcmc.Id
                        join e in _db.Employers on rcmc.EmployerId equals e.Id
                        join s in _db.CandidateApplyJobSources on caj.CandidateApplyJobSourceId equals s.Id
                        join cvPDf in _db.CandidateCVPDFs on caj.CandidateCVPDFId equals cvPDf.Id
                        join c in _db.Candidates on cvPDf.CandidateId equals c.Id
                        join cs in _db.CandidateApplyJobStatuses on caj.CandidateApplyJobStatusId equals cs.Id
                        join com in _db.Companies on e.Id equals com.EmployerId
                        where caj.Active && j.Active && rcmc.Active && e.Active && s.Active && c.Active && cs.Active && cvPDf.Active && cvPDf.CandidateId == parameters.CandidateId && com.Active
                        orderby caj.CreatedTime descending
                        select new CandidateApplyJobAggregate()
                        {
                            Id = caj.Id,
                            EmployerId = e.Id,
                            CandidateApplyJobSourceName = s.Name,
                            CandidateAvatar = c.Photo,
                            CandidateEmail = c.Email,
                            CandidateName = c.FullName,
                            CandidatePhone = c.Phone,
                            CreatedTime = caj.CreatedTime,
                            IsEmployerViewed = caj.IsEmployerViewed,
                            RecruimentCampaignId = rcmc.Id,
                            RecruimentCampaignName = rcmc.Name,
                            CandidateApplyJobStatusColor = cs.Color,
                            CandidateApplyJobStatusName = cs.Name,
                            Description = caj.Description,
                            JobName = j.Name,
                            CandidateApplyJobStatusId = cs.Id,
                            CandidateCVPDFId = caj.CandidateCVPDFId,
                            CandidateCVPDFUrl = cvPDf.Url,
                            CandidateId = c.Id,
                            CompanyId = com.Id,
                            CompanyName = com.Name,
                            JobId = caj.JobId,
                            CompanyAddress = com.AddressDetail,
                            CompanyPhone = com.Phone,
                            CompanyWebsite = com.Website,
                            CityRequired = (from wp in _db.WorkPlaces
                                            join rqc in _db.JobRequireCities on wp.Id equals rqc.CityId
                                            where wp.Active && rqc.Active && rqc.JobId == j.Id
                                            select wp.Name).ToList(),
                            SalaryFrom = j.SalaryFrom,
                            SalaryTo = j.SalaryTo,
                            SalaryTypeId = j.SalaryTypeId
                        };

            int recordsTotal = await query.CountAsync();
        
            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => EF.Functions.Collate(c.JobName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || EF.Functions.Collate(c.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) 
                || EF.Functions.Collate(c.CandidateApplyJobStatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) || EF.Functions.Collate(c.CompanyAddress, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) 
                || EF.Functions.Collate(c.CompanyWebsite, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) 
                || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword) 
                || ((from wp in _db.WorkPlaces
                                            join rqc in _db.JobRequireCities on wp.Id equals rqc.CityId
                                            where wp.Active && rqc.Active && rqc.JobId == c.JobId && EF.Functions.Collate(wp.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                                            select wp.Name).Count() > 0 ) 
                                            );
            }
            
            int recordsFiltered = await query.CountAsync();
            //3.Sort
            //4.Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<CandidateApplyJobAggregate> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
        public async Task<CandidateApplyJobAggregate> DetailById(long id)
        {
            var query = from caj in _db.CandidateApplyJobs
                        join j in _db.Jobs on caj.JobId equals j.Id
                        join rcmc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rcmc.Id
                        join e in _db.Employers on rcmc.EmployerId equals e.Id
                        join s in _db.CandidateApplyJobSources on caj.CandidateApplyJobSourceId equals s.Id
                        join cvPDf in _db.CandidateCVPDFs on caj.CandidateCVPDFId equals cvPDf.Id
                        join c in _db.Candidates on cvPDf.CandidateId equals c.Id
                        join cs in _db.CandidateApplyJobStatuses on caj.CandidateApplyJobStatusId equals cs.Id
                        where caj.Active && j.Active && rcmc.Active && e.Active && s.Active && c.Active && cs.Active && cvPDf.Active && caj.Id == id
                        orderby caj.CreatedTime descending
                        select new CandidateApplyJobAggregate()
                        {
                            Id = caj.Id,
                            EmployerId = e.Id,
                            CandidateApplyJobSourceName = s.Name,
                            CandidateAvatar = c.Photo,
                            CandidateEmail = c.Email,
                            CandidateName = c.FullName,
                            CandidatePhone = c.Phone,
                            CreatedTime = caj.CreatedTime,
                            IsEmployerViewed = caj.IsEmployerViewed,
                            RecruimentCampaignId = rcmc.Id,
                            RecruimentCampaignName = rcmc.Name,
                            CandidateApplyJobStatusColor = cs.Color,
                            CandidateApplyJobStatusName = cs.Name,
                            Description = caj.Description,
                            JobName = j.Name,
                            CandidateApplyJobStatusId = cs.Id
                        };
            var  result = await query.FirstOrDefaultAsync();
            return result; 
        }

        public async Task<List<CandidateApplyJobAggregate>> GetListCandidateApplyJobCompare(long jobId, long candidateApplyJobId)
        {
            var query = (from row in _db.CandidateApplyJobs
                         join ccpdf in _db.CandidateCVPDFs on row.CandidateCVPDFId equals ccpdf.Id
                         join c in _db.Candidates on ccpdf.CandidateId equals c.Id
                         where row.Active && row.JobId == jobId && row.Id != candidateApplyJobId
                         && ccpdf.Active
                         && c.Active && c.IsActivated
                         orderby row.CreatedTime
                         select new CandidateApplyJobAggregate()
                         {
                             Id = row.Id,
                             CandidateId = c.Id,
                             JobId = jobId,
                             CandidateAvatar = c.Photo,
                             CandidateName = c.FullName,
                             CandidateEmail = c.Email,
                             CandidatePhone = c.Phone,
                             CandidateCVPDFId = row.CandidateCVPDFId,
                             CandidateApplyJobStatusId = row.CandidateApplyJobStatusId,
                             CandidateApplyJobSourceId = row.CandidateApplyJobSourceId,
                             CandidateApplyJobStatusName = row.CandidateApplyJobStatus.Name,
                             CandidateApplyJobStatusColor = row.CandidateApplyJobStatus.Color,
                             IsEmployerViewed = row.IsEmployerViewed,
                             CreatedTime = row.CreatedTime,
                             Description = row.Description,
                             CandidateCVPDFUrl = ccpdf.Url,

                         });
            var result = await query.ToListAsync();

            return result;
        }

        public async Task<CandidateApplyJobAggregate> DetailById(long jobId, long candidateApplyJobId)
        {
            var query = (from row in _db.CandidateApplyJobs
                         join ccpdf in _db.CandidateCVPDFs on row.CandidateCVPDFId equals ccpdf.Id
                         join c in _db.Candidates on ccpdf.CandidateId equals c.Id
                         where row.Active && row.JobId == jobId && row.Id == candidateApplyJobId
                         && ccpdf.Active
                         orderby row.CreatedTime
                         select new CandidateApplyJobAggregate()
                         {
                             Id = row.Id,
                             CandidateId = c.Id,
                             JobId = jobId,
                             CandidateAvatar = c.Photo,
                             CandidateName = c.FullName,
                             CandidateEmail = c.Email,
                             CandidatePhone = c.Phone,
                             CandidateCVPDFId = row.CandidateCVPDFId,
                             CandidateApplyJobStatusId = row.CandidateApplyJobStatusId,
                             CandidateApplyJobSourceId = row.CandidateApplyJobSourceId,
                             IsEmployerViewed = row.IsEmployerViewed,
                             CreatedTime = row.CreatedTime,
                             Description = row.Description,
                             CandidateCVPDFUrl = ccpdf.Url,
                             CandidateAddress = c.AddressDetail
                         });
            var result = await query.FirstOrDefaultAsync();

            return result;
        }

    }
}
