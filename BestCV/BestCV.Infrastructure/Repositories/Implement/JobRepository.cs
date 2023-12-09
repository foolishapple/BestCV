using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using BestCV.Domain.Aggregates.ExperienceRange;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.JobReasonsApply;
using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Aggregates.JobRequireJobSkill;
using BestCV.Domain.Aggregates.JobRequireSkill;
using BestCV.Domain.Aggregates.JobSecondaryPosition;
using BestCV.Domain.Aggregates.JobTag;
using BestCV.Domain.Aggregates.JobType;
using BestCV.Domain.Aggregates.Position;
using BestCV.Domain.Aggregates.SalaryRange;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class JobRepository : RepositoryBaseAsync<Job, long, JobiContext>, IJobRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public JobRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            _db = dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: List Job aggregate by recruitCampain Id
        /// </summary>
        /// <param name="id">recruitCampain id</param>
        /// <returns></returns>
        public async Task<List<RecruitCampainJobAggregate>> ListByRecruitCampain(long id)
        {
            var query = from j in _db.Jobs
                        join js in _db.JobStatuses on j.JobStatusId equals js.Id
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        where j.Active && js.Active && rc.Id == id
                        orderby j.CreatedTime descending
                        select new RecruitCampainJobAggregate()
                        {
                            Id = j.Id,
                            StatusColor = js.Color,
                            StatusName = js.Name,
                            IsApproved = j.IsApproved,
                            Name = j.Name,
                            RecruitCampainId = rc.Id,
                            TotalCandidateApply = (from cv in _db.CandidateApplyJobs
                                                   join source in _db.CandidateApplyJobSources on cv.CandidateApplyJobSourceId equals source.Id
                                                   where cv.Active && source.Active && cv.JobId == j.Id
                                                   select cv.Id
                                                   ).Count(),
                            ViewCount = j.ViewCount
                        };
            var data = await query.ToListAsync();
            return data;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/08/2023
        /// Description: List job datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DTResult<EmployerJobAggregate>> ListDTPaging(DTJobPagingParameters parameters)
        {
            //0. Option
            string keyword = "";
            string noneVietNameseKeyword = "";
            if (parameters.Search != null && !string.IsNullOrEmpty(parameters.Search.Value))
            {
                keyword = parameters.Search.Value.Trim();
                noneVietNameseKeyword = keyword.ToLower().RemoveVietnamese();
            }
            //1. Join
            var query = from j in _db.Jobs
                        join js in _db.JobStatuses on j.JobStatusId equals js.Id
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        where j.Active && js.Active && rc.Active
                        orderby j.CreatedTime descending
                        select new EmployerJobAggregate()
                        {
                            StatusColor = js.Color,
                            StatusId = js.Id,
                            StatusName = js.Name,
                            ApplyEndDate = j.ApplyEndDate,
                            CreatedTime = j.CreatedTime,
                            Id = j.Id,
                            IsApproved = j.IsApproved,
                            Name = j.Name,
                            RecruitmentCampaginId = rc.Id,
                            RecruitmentCampaginName = rc.Name,
                            TotalCandidateApply = (from caj in _db.CandidateApplyJobs
                                                   join cajs in _db.CandidateApplyJobSources on caj.CandidateApplyJobSourceId equals cajs.Id
                                                   where caj.Active && cajs.Active && cajs.Id == CandidateApplyJobSourceConst.CANDIDATE_APPLY && caj.JobId == j.Id
                                                   select caj.Id).Count(),
                            EmployerId = rc.EmployerId,
                            ViewCount = j.ViewCount
                        };
            if (parameters.EmployerId != null)
            {
                query = query.Where(c => c.EmployerId == parameters.EmployerId);
            }
            if (parameters.JobStatusId != null)
            {
                query = query.Where(c => c.StatusId == parameters.JobStatusId);
            }
            int recordsTotal = await query.CountAsync();
            //2. Fillter
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c =>
                    EF.Functions.Collate(c.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                    || ("#" + c.Id.ToString()).Contains(noneVietNameseKeyword)
                    || (c.IsApproved ? EF.Functions.Collate(c.StatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)) : "tin chua duoc duyet".Contains(noneVietNameseKeyword))
                    || EF.Functions.Collate(c.RecruitmentCampaginName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                    || ("#" + c.RecruitmentCampaginId.ToString()).Contains(noneVietNameseKeyword)
                    || ("luot ung tuyen: " + c.TotalCandidateApply.ToString()).Contains(noneVietNameseKeyword)
                    || (c.CreatedTime.ToDateString() + " - " + ((c.ApplyEndDate != null) ? c.CreatedTime.ToDateString() : "hien tai")).Contains(noneVietNameseKeyword)
                );
            }
            int recordsFiltered = await query.CountAsync();
            //3. Sort

            //4. Return data
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            return new DTResult<EmployerJobAggregate>()
            {
                data = data,
                draw = parameters.Draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
        }



        public async Task<DetailJobAggregates> GetDetailJobAsync(long jobId, long candidateId)
        {
            return await (from row in _db.Jobs
                          from jc in _db.JobCategories
                          from js in _db.JobStatuses
                          from jt in _db.JobTypes
                          from jp in _db.JobPosition
                          from er in _db.ExperienceRange
                          from st in _db.SalaryType
                          from e in _db.Employers
                          from c in _db.Companies
                          from rc in _db.RecruitmentCampaigns
                          where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && c.Active && rc.Active
                          && row.JobPositionId == jc.Id
                          && row.JobStatusId == js.Id
                          && row.JobTypeId == jt.Id
                          && row.PrimaryJobCategoryId == jp.Id
                          && row.ExperienceRangeId == er.Id
                          && row.SalaryTypeId == st.Id
                          && row.RecruimentCampaignId == rc.Id
                          && c.EmployerId == rc.EmployerId
                          && rc.EmployerId == e.Id
                          && row.IsApproved
                          && rc.IsAprroved
                          && row.Id == jobId
                          select new DetailJobAggregates
                          {
                              Id = row.Id,
                              Active = row.Active,
                              JobPositionId = row.JobPositionId,
                              JobStatusId = row.JobStatusId,
                              SalaryTypeId = row.SalaryTypeId,
                              ExperienceRangeId = row.ExperienceRangeId,
                              PrimaryJobCategoryId = row.PrimaryJobCategoryId,
                              ApplyEndDate = row.ApplyEndDate,
                              ApprovalDate = row.ApprovalDate,
                              Benefit = row.Benefit,
                              CreatedTime = row.CreatedTime,
                              Currency = row.Currency,
                              Description = row.Description,
                              GenderRequirement = row.GenderRequirement,
                              IsApproved = row.IsApproved,
                              JobTypeId = row.JobTypeId,
                              Name = row.Name,
                              Overview = row.Overview,
                              ReceiverEmail = row.ReceiverEmail,
                              ReceiverPhone = row.ReceiverPhone,
                              ReceiverName = row.ReceiverName,
                              Requirement = row.Requirement,
                              TotalRecruitment = row.TotalRecruitment,
                              SalaryTypeName = st.Name,
                              ExperienceRangeName = er.Name,
                              PrimaryJobCategoryName = jc.Name,
                              PrimaryJobCategoryIcon = jc.Icon,
                              JobStatusName = js.Name,
                              JobTypeName = jt.Name,
                              JobPositionName = jp.Name,
                              RecruimentCampaignId = row.RecruimentCampaignId,
                              SalaryFrom = row.SalaryFrom,
                              SalaryTo = row.SalaryTo,
                              Search = row.Search,
                              CompanyId = c.Id,
                              CompanyName = c.Name,
                              EmloyerId = e.Id,
                              EmployerName = e.Fullname,
                              CompanyLogo = c.Logo,
                              CompanyAddressDetail = c.AddressDetail,
                              CompanyCoverPhoto = c.CoverPhoto,
                              CompanyDescription = c.Description,
                              CompanyEmailAddress = c.EmailAddress,
                              CompanyFacebookLink = c.FacebookLink,
                              CompanyFoundedIn = c.FoundedIn,
                              CompanyLinkedInLink = c.LinkedinLink,
                              CompanyPhone = c.Phone,
                              CompanySizeId = c.CompanySizeId,
                              CompanySizeName = c.CompanySize.Name,
                              CompanyTaxCode = c.TaxCode,
                              CompanyTiktokLink = c.TiktokLink,
                              CompanyWebsite = c.Website,
                              CompanyWorkPlace = c.WorkPlace.Name,
                              CompanyWorkPlaceId = c.WorkPlaceId,
                              CompanyYoutubeLink = c.YoutubeLink,

                              JobRequireCity = (from jrc in _db.JobRequireCities
                                                from job in _db.Jobs
                                                from wp in _db.WorkPlaces
                                                where jrc.Active && job.Active && wp.Active && jrc.JobId == job.Id && jrc.JobId == row.Id && jrc.CityId == wp.Id && job.Id == jobId
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
                              IsSaveJob = _db.CandidateSaveJobs.Any(x => x.Active && x.CandidateId == candidateId && x.JobId == row.Id),
                              IsApplyJob = _db.CandidateApplyJobs.Any(x => x.Active && x.CandidateCVPDF.CandidateId == candidateId && x.JobId == row.Id),
                              JobRequireSkill = (from job in _db.Jobs
                                                 from jobSkill in _db.JobSkill
                                                 from jobrequire in _db.JobRequireJobSkills
                                                 where jobrequire.JobId == job.Id && jobrequire.JobSkillId == jobSkill.Id && jobrequire.JobId == jobId
                                                 select new JobRequireJobSkillAggregates
                                                 {
                                                     Id = jobrequire.Id,
                                                     Active = jobrequire.Active,
                                                     JobId = jobrequire.JobId,
                                                     JobSkillId = jobrequire.JobSkillId,
                                                     JobName = job.Name,
                                                     JobSkillName = jobSkill.Name,
                                                     CreatedTime = jobrequire.CreatedTime,
                                                 }).ToList(),
                              FieldOfCompany = (from cfoa in _db.CompanyFieldOfActivities
                                                from foa in _db.FieldOfActivities
                                                where cfoa.FieldOfActivityId == foa.Id && cfoa.Active && foa.Active && cfoa.CompanyId == c.Id
                                                select new CompanyFieldOfActivityAggregates
                                                {
                                                    Id = cfoa.Id,
                                                    Active = cfoa.Active,
                                                    CompanyId = cfoa.CompanyId,
                                                    FieldOfActivityId = cfoa.FieldOfActivityId,
                                                    FieldOfActivityName = foa.Name,
                                                    CreatedTime = cfoa.CreatedTime
                                                }).ToList(),
                          }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// authir: truongthieuhuyen
        /// created: 23.08.2023
        /// description: lấy chi tiết job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<JobDetailAggregates> GetDetailById(long jobId)
        {
            // job entity
            var job = await _db.Jobs.Where(x => x.Id.Equals(jobId) && x.Active).FirstOrDefaultAsync();

            // list Tag
            var tags = await _db.JobTags.Where(x => x.JobId.Equals(jobId) && x.Active)
                .Select(x => x.TagId).ToListAsync();

            // list required skill
            var skills = await _db.JobRequireJobSkills.Where(x => x.JobId.Equals(jobId) && x.Active)
                .Select(x => x.JobSkillId).ToListAsync();

            // list secondary Category
            var sencondaryCategories = await _db.JobSecondaryJobCategories.Where(x => x.JobId.Equals(jobId) && x.Active)
                .Select(x => x.JobCategoryId).ToListAsync();

            // list reason apply
            var reasons = await _db.JobReasonApplies.Where(x => x.JobId.Equals(jobId) && x.Active).ToListAsync();

            // list work place
            var workplaces = new List<JobWorkPlaceAggregates>();
            var cities = await _db.JobRequireCities.Where(x => x.JobId.Equals(jobId) && x.Active).ToListAsync();
            foreach (var city in cities)
            {
                var districts = await _db.JobRequireDistricts.Where(x => x.JobRequireCityId.Equals(city.Id) && x.Active).ToListAsync();
                foreach (var item in districts)
                {
                    workplaces.Add(new JobWorkPlaceAggregates()
                    {
                        JobRequireCityId = city.Id,
                        CityId = city.CityId,
                        JobRequireDistrictId = item.Id,
                        DistrictId = districts != null ? item.DistrictId : null,
                        AddressDetail = districts != null ? item.AddressDetail : null,
                    });
                }
            }


            // mapping
            if (job != null)
            {
                var data = new JobDetailAggregates()
                {
                    RecruimentCampaignId = job.RecruimentCampaignId,
                    Id = job.Id,
                    Active = job.Active,
                    IsApproved = job.IsApproved,
                    CreatedTime = job.CreatedTime,
                    Description = job.Description,
                    Search = job.Search,
                    Name = job.Name,
                    PrimaryJobCategoryId = job.PrimaryJobCategoryId,
                    TotalRecruitment = job.TotalRecruitment,
                    GenderRequirement = job.GenderRequirement,
                    ExperienceRangeId = job.ExperienceRangeId,
                    JobStatusId = job.JobStatusId,
                    JobPositionId = job.JobPositionId,
                    JobTypeId = job.JobTypeId,
                    Currency = job.Currency,
                    SalaryTypeId = job.SalaryTypeId,
                    SalaryFrom = job.SalaryFrom,
                    SalaryTo = job.SalaryTo,
                    ReceiverEmail = job.ReceiverEmail,
                    ReceiverName = job.ReceiverName,
                    ReceiverPhone = job.ReceiverPhone,
                    ApplyEndDate = job.ApplyEndDate,
                    Overview = job.Overview,
                    Requirement = job.Requirement,
                    Benefit = job.Benefit,
                    ListTag = tags,
                    ListJobReasonApply = reasons,
                    ListJobRequireSkill = skills,
                    ListJobSecondaryJobCategory = sencondaryCategories,
                    ListJobRequireWorkplace = workplaces,
                    ViewCount = job.ViewCount
                };
                return data;
            }
            return null;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobCategorySelected()
        {
            return await _db.JobCategories.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobTypeSelected()
        {
            return await _db.JobTypes.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobStatusSelected()
        {
            return await _db.JobStatuses.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobExperienceSelected()
        {
            return await _db.ExperienceRange.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListCampaignSelected()
        {
            return await _db.RecruitmentCampaigns.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<object> ListRecruitmentNewsAggregates(DTParameters parameters)
        {
            var keyword = parameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            if (parameters.Order != null)
            {
                orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = await (
                from j in _db.Jobs
                join js in _db.JobStatuses on j.JobStatusId equals js.Id
                join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                join jc in _db.JobCategories on j.PrimaryJobCategoryId equals jc.Id
                join jt in _db.JobTypes on j.JobTypeId equals jt.Id
                join e in _db.Employers on rc.EmployerId equals e.Id
                join c in _db.Companies on e.Id equals c.EmployerId

                where j.Active && js.Active && rc.Active && jc.Active && jt.Active && e.Active && c.Active
                select new RecruitmentNewsAggregates
                {
                    Id = j.Id,
                    Active = j.Active,
                    CreatedTime = j.CreatedTime,
                    Description = j.Description,
                    Name = j.Name,
                    CompanyName = c.Name,
                    IsApproved = j.IsApproved,
                    JobCategoryId = j.PrimaryJobCategoryId,
                    JobCategoryName = jc.Name,
                    RecruimentCampaignId = j.RecruimentCampaignId,
                    RecruimentCampaignName = rc.Name,
                    JobStatusId = j.JobStatusId,
                    JobStatusName = js.Name,
                    JobTypeId = j.JobTypeId,
                    JobTypeName = jt.Name,
                    Search = j.Search,
                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword)
            || s.Name.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.JobCategoryName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.CompanyName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.RecruimentCampaignName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.JobStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.JobTypeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.CreatedTime.ToCustomString().Contains(keyword)).ToList();

            foreach (var column in parameters.Columns)
            {
                var search = column.Search.Value;
                if (!search.Contains("/"))
                {
                    search = column.Search.Value.ToLower().RemoveVietnamese();
                }
                if (string.IsNullOrEmpty(search)) continue;
                switch (column.Data)
                {
                    case "name":
                        result = result.Where(r => r.Name.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "recruimentCampaignName":
                        result = result.Where(r => r.RecruimentCampaignName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "companyName":
                        result = result.Where(r => r.CompanyName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "jobCategoryName":
                        var jobCategoryIdArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => jobCategoryIdArr.Contains(r.JobCategoryId)).ToList();
                        break;
                    case "jobStatusName":
                        var jobStatusIdArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => jobStatusIdArr.Contains(r.JobStatusId)).ToList();
                        break;
                    case "jobTypeName":
                        var jobTypeIdArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => jobTypeIdArr.Contains(r.JobTypeId)).ToList();
                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;
                    case "isApproved":
                        if (search != "null")
                        {
                            result = result.Where(r => r.IsApproved.ToString().ToLower().Contains(search)).ToList();
                        }
                        break;
                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();
            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                allData = result,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        public async Task<object> LoadDataFilterJobHomePageAsync()
        {
            //lấy data loại công việc
            var jobTypeData = await (from jt in _db.JobTypes
                                     from j in _db.Jobs
                                     where jt.Active && j.Active && j.IsApproved
                                     select new FilterByJobTypeAggregates
                                     {
                                         Id = jt.Id,
                                         Name = jt.Name,
                                         CountJob = (from row in _db.Jobs
                                                     from row1 in _db.JobTypes
                                                     where row1.Id == row.JobTypeId && row.Active && row1.Active && row.IsApproved && row.JobTypeId == jt.Id
                                                     select row).Count()
                                     }).Distinct().ToListAsync();

            //Lấy data kinh nghiệm
            var jobExperience = await (from j in _db.Jobs
                                       from ex in _db.ExperienceRange
                                       where j.Active && ex.Active && j.IsApproved
                                       select new FilterByExperienceRangeAggregates
                                       {
                                           Id = ex.Id,
                                           Name = ex.Name,
                                           CountJob = (from row in _db.Jobs
                                                       from row1 in _db.ExperienceRange
                                                       where row1.Id == row.ExperienceRangeId && row.Active && row1.Active && row.IsApproved && row.ExperienceRangeId == ex.Id
                                                       select row).Count()
                                       }).Distinct().ToListAsync();

            //Lấy data mức lương
            var jobSalary = await (from j in _db.Jobs
                                   from sr in _db.SalaryRange
                                   where j.Active && sr.Active && j.IsApproved
                                   select new FilterBySalaryRangeAggragates
                                   {
                                       Id = sr.Id,
                                       Name = sr.Name,
                                       CountJob = (from row in _db.Jobs
                                                   from row1 in _db.SalaryRange
                                                   where row.Active && row1.Active && row.IsApproved

                                                   select row).Count()
                                   }).Distinct().ToListAsync();

            //Lấy data chức vụ
            var jobPrimaryPosition = await (from j in _db.Jobs
                                            from p in _db.JobPosition
                                            where j.Active && p.Active && j.IsApproved
                                            select new FilterByJobPositionAggragates
                                            {
                                                Id = p.Id,
                                                Name = p.Name,
                                                CountJob = (from row in _db.Jobs
                                                            from row1 in _db.JobPosition
                                                            where row.Active && row1.Active && row.IsApproved && row.PrimaryJobCategoryId == row1.Id && row.PrimaryJobCategoryId == p.Id
                                                            select row).Count()
                                            }).Distinct().ToListAsync();
            return new
            {
                JobTypeData = jobTypeData,
                JobExperience = jobExperience,
                JobSalary = jobSalary,
                JobPrimaryPosition = jobPrimaryPosition,
            };
        }

        /// <summary>
        /// Edited: TUNGTD 20/09/2023 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<PagingData<List<SearchJobAggregates>>> SearchJobHomePageAsync(SearchingJobParameters parameter)
        {
            //khai báo biến
            var keyword = parameter.Keywords;
            var pageIndex = parameter.PageIndex;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            var location = parameter.Location;
            var pageSize = parameter.PageSize;
            var candidateId = parameter.CandidateId;


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
            //lấy dữ liệu
            var query = from row in _db.Jobs
                        join jc in _db.JobCategories on row.JobPositionId equals jc.Id
                        join js in _db.JobStatuses on row.JobStatusId equals js.Id
                        join jt in _db.JobTypes on row.JobTypeId equals jt.Id
                        join jp in _db.JobPosition on row.JobPositionId equals jp.Id
                        join er in _db.ExperienceRange on row.ExperienceRangeId equals er.Id
                        join st in _db.SalaryType on row.SalaryTypeId equals st.Id
                        join rc in _db.RecruitmentCampaigns on row.RecruimentCampaignId equals rc.Id
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        join c in _db.Companies on e.Id equals c.EmployerId
                        where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && c.Active && rc.Active
                        && row.IsApproved
                        && rc.IsAprroved
                        select new SearchJobAggregates
                        {
                            Id = row.Id,
                            RefreshDate = row.RefreshDate,
                            PrimaryJobCategoryId = row.PrimaryJobCategoryId,
                            JobStatusId = row.JobStatusId,
                            SalaryTypeId = row.SalaryTypeId,
                            ExperienceRangeId = row.ExperienceRangeId,
                            JobPositionId = row.JobPositionId,
                            ApplyEndDate = row.ApplyEndDate,
                            //ApprovalDate = row.ApprovalDate,
                            //Benefit = row.Benefit,
                            CreatedTime = row.CreatedTime,
                            Currency = row.Currency,
                            //Description = row.Description,
                            //GenderRequirement = row.GenderRequirement,
                            //IsApproved = row.IsApproved,
                            JobTypeId = row.JobTypeId,
                            Name = row.Name,
                            //Overview = row.Overview,
                            //ReceiverEmail = row.ReceiverEmail,
                            //ReceiverPhone = row.ReceiverPhone,
                            //ReceiverName = row.ReceiverName,
                            //Requirement = row.Requirement,
                            TotalRecruitment = row.TotalRecruitment,
                            SalaryTypeName = st.Name,
                            ExperienceRangeName = er.Name,
                            PrimaryJobCategoryName = jc.Name,
                            JobStatusName = js.Name,
                            JobTypeName = jt.Name,
                            JobPositionName = jp.Name,
                            //RecruimentCampaignId = row.RecruimentCampaignId,
                            SalaryFrom = row.SalaryFrom,
                            SalaryTo = row.SalaryTo,
                            //Search = row.Search,
                            CompanyId = c.Id,
                            CompanyName = c.Name,
                            //EmloyerId = e.Id,
                            //EmployerName = e.Fullname,
                            CompanyLogo = c.Logo,
                            JobRequireCity = (from jrc in _db.JobRequireCities
                                              join job in _db.Jobs on jrc.JobId equals job.Id
                                              join wp in _db.WorkPlaces on jrc.CityId equals wp.Id
                                              where jrc.Active && job.Active && wp.Active && jrc.JobId == row.Id
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
                            ListBenefit = (from b in _db.Benefits
                                           join spb in _db.ServicePackageBenefits on b.Id equals spb.BenefitId
                                           join esp in _db.EmployerServicePackages on spb.EmployerServicePackageId equals esp.Id
                                           from jesp in _db.JobServicePackages
                                           from job in _db.Jobs
                                           let isExpired = jesp.CreatedTime.AddDays(jesp.ExpireTime ?? 0) > DateTime.Now
                                           where b.Active && esp.Active && job.Active && jesp.Active
                                           && job.Id == row.Id && jesp.JobId == job.Id && jesp.EmployerServicePackageId == esp.Id && b.Id == spb.BenefitId && esp.Id == spb.EmployerServicePackageId && spb.Active && isExpired
                                           select b.Id).Distinct().ToList(),
                            IsSaveJob = _db.CandidateSaveJobs.Any(x => x.Active && x.CandidateId == candidateId && x.JobId == row.Id)
                        };

            var allRecord = await query.CountAsync();
            //tìm kiếm tất cả
            if (!string.IsNullOrEmpty(keyword))
            {
                if (keyword == TagConst.URGENT)
                {
                    query = query.Where(s => s.ListBenefit.Where(c => c == BenefitId.TUYEN_DUNG_GAP).Count() > 0 || EF.Functions.Collate(s.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobPositionName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobTypeName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobStatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.ExperienceRangeName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)));
                }
                else
                {
                    query = query.Where(s => EF.Functions.Collate(s.CompanyName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobPositionName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobTypeName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.JobStatusName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                || EF.Functions.Collate(s.ExperienceRangeName, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General)));
                }
            }
            //filter theo danh mục
            if (parameter.JobCategoryId != 0)
            {
                query = query.Where(x => x.PrimaryJobCategoryId == parameter.JobCategoryId);
            }
            //tìm kiếm theo địa điểm
            if (location != 0)
            {
                query = query.Where(s => (s.JobRequireCity.Where(x => x.CityId == location).Count() > 0));
            }

            //filter theo loại công việc
            if (parameter.JobType.Length > 0)
            {
                query = query.Where(x => parameter.JobType.Contains(x.JobTypeId.ToString()));
            }

            //filter theo chức vụ
            if (parameter.JobPosition.Length > 0)
            {
                query = query.Where(x => parameter.JobPosition.Contains(x.JobPositionName.ToString()));
            }

            //filter theo kinh nghiệm
            if (parameter.JobExperience.Length > 0)
            {
                query = query.Where(x => parameter.JobExperience.Contains(x.ExperienceRangeId.ToString()));
            }

            //filter theo mức lương
            if (parameter.SalaryRange.Length > 0)
            {
                var salaryArr = parameter.SalaryRange.Split(",").Select(n => Int32.Parse(n));
                query = query.Where(x => (from slr in _db.SalaryRange
                                          where slr.Active && salaryArr.Contains(slr.Id) && (
                                          slr.Id == SalaryRangeId.THREE_TO_FIVE ? ((x.SalaryTypeId == SalaryTypeId.FROM && x.SalaryFrom >= 3000000 && x.SalaryFrom <= 5000000) || (x.SalaryTypeId == SalaryTypeId.TO && x.SalaryTo >= 3000000 && x.SalaryTo <= 5000000) || (x.SalaryTypeId == SalaryTypeId.BETWEEN && (x.SalaryFrom >= 3000000 && x.SalaryFrom <= 5000000) || (x.SalaryTo >= 3000000 && x.SalaryTo <= 5000000)))
                                          : (slr.Id == SalaryRangeId.FIVE_TO_SEVEN ? ((x.SalaryTypeId == SalaryTypeId.FROM && x.SalaryFrom >= 5000000 && x.SalaryFrom <= 7000000) || (x.SalaryTypeId == SalaryTypeId.TO && x.SalaryTo <= 7000000 && x.SalaryTo >= 5000000) || (x.SalaryTypeId == SalaryTypeId.BETWEEN && (x.SalaryFrom >= 5000000 && x.SalaryFrom <= 7000000) || (x.SalaryTo <= 7000000 && x.SalaryTo >= 5000000)))
                                          : (slr.Id == SalaryRangeId.SEVEN_TO_TEN ? ((x.SalaryTypeId == SalaryTypeId.FROM && x.SalaryFrom >= 7000000 && x.SalaryFrom <= 10000000) || (x.SalaryTypeId == SalaryTypeId.TO && x.SalaryTo <= 10000000 && x.SalaryTo >= 7000000) || (x.SalaryTypeId == SalaryTypeId.BETWEEN && (x.SalaryFrom >= 7000000 && x.SalaryFrom <= 10000000) || (x.SalaryTo <= 10000000 && x.SalaryTo >= 7000000)))
                                          : (slr.Id == SalaryRangeId.TEN_TO_FIFTEEN ? ((x.SalaryTypeId == SalaryTypeId.FROM && x.SalaryFrom >= 10000000 && x.SalaryFrom <= 15000000) || (x.SalaryTypeId == SalaryTypeId.TO && x.SalaryTo <= 15000000 && x.SalaryTo >= 10000000) || (x.SalaryTypeId == SalaryTypeId.BETWEEN && (x.SalaryFrom >= 10000000 && x.SalaryFrom <= 15000000) || (x.SalaryTo <= 15000000 && x.SalaryTo >= 10000000)))
                                          : (slr.Id == SalaryRangeId.FIFTEEN_TO_TWENTY_FIVE ? ((x.SalaryTypeId == SalaryTypeId.FROM && x.SalaryFrom >= 15000000 && x.SalaryFrom <= 25000000) || (x.SalaryTypeId == SalaryTypeId.TO && x.SalaryTo <= 25000000 && x.SalaryTo >= 15000000) || (x.SalaryTypeId == SalaryTypeId.BETWEEN && (x.SalaryFrom >= 15000000 && x.SalaryFrom <= 25000000) || (x.SalaryTo <= 25000000 && x.SalaryTo >= 15000000))) : (slr.Id == SalaryRangeId.AGGREMENT ? x.SalaryTypeId == SalaryTypeId.AGREEMENT : false))))))
                                          select slr).Count() > 0);
            }

            //orderby  
            if (parameter.OrderCriteria != null)
            {
                long top1FeatureId = await (from tfj in _db.TopFeatureJobs
                                            join j in query on tfj.JobId equals j.Id
                                            where tfj.Active
                                            select tfj
                                      ).OrderBy(c => c.OrderSort).ThenBy(c => c.SubOrderSort).Select(c => c.JobId).FirstOrDefaultAsync();
                long[] topAreaIds = await (from tfj in _db.TopAreaJobs
                                         join j in query on tfj.JobId equals j.Id
                                         where tfj.Active && tfj.JobId != top1FeatureId
                                         select tfj
                                      ).OrderBy(c=>c.OrderSort).ThenBy(c=>c.SubOrderSort).Select(c=>c.JobId).Take(CronUtil.Benefit.TOTAL_AREA_ON_TOP).ToArrayAsync();
                switch (parameter.OrderCriteria)
                {
                    case "asc":
                        query = query = query.OrderBy(x => x.RefreshDate)
                            .ThenBy(c => c.CreatedTime)
                            .ThenBy(c => c.Id == top1FeatureId)
                            .ThenBy(c => topAreaIds.Contains(c.Id))
                            .ThenBy(c => c.ListBenefit.Count() > 0);
                        break;
                    case "desc":
                        query = query.OrderByDescending(x => x.RefreshDate)
                            .ThenByDescending(c => c.CreatedTime)
                            .ThenByDescending(c => c.Id == top1FeatureId)
                            .ThenByDescending(c => topAreaIds.Contains(c.Id))
                            .ThenByDescending(c => c.ListBenefit.Count() > 0);
                        break;
                    case "default":
                        query = query.OrderByDescending(c =>c.Id == top1FeatureId)
                            .ThenByDescending(c=>topAreaIds.Contains(c.Id))
                            .ThenByDescending(c=> c.ListBenefit.Count() > 0)
                            .ThenByDescending(c=>c.CreatedTime);
                        break;
                }
            }
            var recordsTotal = await query.CountAsync();
            return new PagingData<List<SearchJobAggregates>>
            {
                DataSource = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(),
                Total = allRecord,
                PageSize = parameter.PageSize,
                CurrentPage = pageIndex,
                TotalFiltered = recordsTotal,
            };
        }

        public int CountJob()
        {
            return _db.Jobs.Count(x => x.Active && x.IsApproved);
        }
        /// <summary>
        /// Author: Nam Anh
        /// Created: 22/8/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> QuickIsApprovedAsync(long id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.IsApproved = obj.IsApproved ? false : true;

                _db.Attach(obj);
                _db.Entry(obj).Property(x => x.IsApproved).IsModified = true;
                return true;
            }
            return false;
        }

        public async Task<List<SearchJobAggregates>> ListJobReference(int categoryId, int typeId)
        {
            return await (from row in _db.Jobs
                          join jc in _db.JobCategories on row.PrimaryJobCategoryId equals jc.Id
                          join jt in _db.JobTypes on row.JobTypeId equals jt.Id
                          join rc in _db.RecruitmentCampaigns on row.RecruimentCampaignId equals rc.Id
                          join e in _db.Employers on rc.EmployerId equals e.Id
                          join c in _db.Companies on e.Id equals c.EmployerId
                          where row.Active && row.IsApproved && jc.Active && jt.Active && rc.Active && e.Active && c.Active
                          orderby row.PrimaryJobCategoryId == categoryId && row.JobTypeId == typeId descending
                          select new SearchJobAggregates
                          {
                              Id = row.Id,
                              Active = row.Active,
                              PrimaryJobCategoryId = row.PrimaryJobCategoryId,
                              JobStatusId = row.JobStatusId,
                              SalaryTypeId = row.SalaryTypeId,
                              ExperienceRangeId = row.ExperienceRangeId,
                              JobPositionId = row.JobPositionId,
                              ApplyEndDate = row.ApplyEndDate,
                              ApprovalDate = row.ApprovalDate,
                              Benefit = row.Benefit,
                              CreatedTime = row.CreatedTime,
                              Currency = row.Currency,
                              Description = row.Description,
                              GenderRequirement = row.GenderRequirement,
                              IsApproved = row.IsApproved,
                              JobTypeId = row.JobTypeId,
                              Name = row.Name,
                              Overview = row.Overview,
                              ReceiverEmail = row.ReceiverEmail,
                              ReceiverPhone = row.ReceiverPhone,
                              ReceiverName = row.ReceiverName,
                              Requirement = row.Requirement,
                              TotalRecruitment = row.TotalRecruitment,
                              PrimaryJobCategoryName = jc.Name,
                              JobTypeName = jt.Name,
                              RecruimentCampaignId = row.RecruimentCampaignId,
                              SalaryFrom = row.SalaryFrom,
                              SalaryTo = row.SalaryTo,
                              Search = row.Search,
                              CompanyId = c.Id,
                              CompanyName = c.Name,
                              EmloyerId = e.Id,
                              EmployerName = e.Fullname,
                              CompanyLogo = c.Logo,
                              JobRequireCity = (from jrc in _db.JobRequireCities
                                                from job in _db.Jobs
                                                from wp in _db.WorkPlaces
                                                where jrc.Active && job.Active && wp.Active && jrc.JobId == job.Id && jrc.JobId == row.Id && jrc.CityId == wp.Id
                                                select new JobRequireCityAggregates
                                                {
                                                    CityId = jrc.CityId,
                                                    JobId = row.Id,
                                                    Active = jrc.Active,
                                                    CityName = wp.Name,
                                                    //CreatedTime = jrc.CreatedTime,
                                                    JobName = job.Name
                                                }).Distinct().ToList(),
                          }).Take(4).ToListAsync();
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 24/08/2023
        /// Description: List job by companyId
        /// </summary>
        /// <param name="companyId">Mã công ty</param>
        /// <param name="candidateId">Mã ứng viên</param>
        /// <param name="quantity">Số lượng bản ghi muốn lấy</param>
        /// <returns></returns>
        public async Task<List<SearchJobAggregates>> ListJobByCompanyId(int companyId, long candidateId, int quantity)
        {
            //lấy dữ liệu
            var data = (
                from row in _db.Jobs
                from jc in _db.JobCategories
                from js in _db.JobStatuses
                from jt in _db.JobTypes
                from jp in _db.JobPosition
                from er in _db.ExperienceRange
                from st in _db.SalaryType
                from e in _db.Employers
                from c in _db.Companies
                from rc in _db.RecruitmentCampaigns
                where row.Active && jc.Active && js.Active && jt.Active && jp.Active && st.Active && er.Active && e.Active && c.Active && rc.Active && c.Id == companyId
                && row.PrimaryJobCategoryId == jc.Id
                && row.JobStatusId == js.Id
                && row.JobTypeId == jt.Id
                && row.JobPositionId == jp.Id
                && row.ExperienceRangeId == er.Id
                && row.SalaryTypeId == st.Id
                && row.RecruimentCampaignId == rc.Id
                && c.EmployerId == rc.EmployerId
                && rc.EmployerId == e.Id
                && row.IsApproved
                && rc.IsAprroved
                select new SearchJobAggregates
                {
                    Id = row.Id,
                    Active = row.Active,
                    PrimaryJobCategoryId = row.PrimaryJobCategoryId,
                    JobStatusId = row.JobStatusId,
                    SalaryTypeId = row.SalaryTypeId,
                    ExperienceRangeId = row.ExperienceRangeId,
                    JobPositionId = row.JobPositionId,
                    ApplyEndDate = row.ApplyEndDate,
                    ApprovalDate = row.ApprovalDate,
                    Benefit = row.Benefit,
                    CreatedTime = row.CreatedTime,
                    Currency = row.Currency,
                    Description = row.Description,
                    GenderRequirement = row.GenderRequirement,
                    IsApproved = row.IsApproved,
                    JobTypeId = row.JobTypeId,
                    Name = row.Name,
                    Overview = row.Overview,
                    ReceiverEmail = row.ReceiverEmail,
                    ReceiverPhone = row.ReceiverPhone,
                    ReceiverName = row.ReceiverName,
                    Requirement = row.Requirement,
                    TotalRecruitment = row.TotalRecruitment,
                    SalaryTypeName = st.Name,
                    ExperienceRangeName = er.Name,
                    PrimaryJobCategoryName = jc.Name,
                    JobStatusName = js.Name,
                    JobTypeName = jt.Name,
                    JobPositionName = jp.Name,
                    RecruimentCampaignId = row.RecruimentCampaignId,
                    SalaryFrom = row.SalaryFrom,
                    SalaryTo = row.SalaryTo,
                    Search = row.Search,
                    CompanyId = c.Id,
                    CompanyName = c.Name,
                    EmloyerId = e.Id,
                    EmployerName = e.Fullname,
                    CompanyLogo = c.Logo,
                    JobRequireCity = (from jrc in _db.JobRequireCities
                                      from job in _db.Jobs
                                      from wp in _db.WorkPlaces
                                      where jrc.Active && job.Active && wp.Active && jrc.JobId == job.Id && jrc.JobId == row.Id && jrc.CityId == wp.Id
                                      select new JobRequireCityAggregates
                                      {
                                          Id = jrc.Id,
                                          CityId = jrc.CityId,
                                          JobId = jrc.Id,
                                          CityName = wp.Name,
                                          CreatedTime = jrc.CreatedTime,
                                          JobName = job.Name
                                      }).ToList(),
                    IsSaveJob = _db.CandidateSaveJobs.Any(x => x.Active && x.CandidateId == candidateId && x.JobId == row.Id)
                });
            //lọc và lấy số lương bản ghi
            var result = await data.OrderByDescending(x => x.CreatedTime)
                             .Take(quantity)
                             .ToListAsync();

            return result;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/08/2023
        /// Description: Count job by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<int> CountByCondition(CountJobCondition condition)
        {
            var query = from j in _db.Jobs
                        join js in _db.JobStatuses on j.JobStatusId equals js.Id
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        where j.Active && js.Active && rc.Active && e.Active
                        select new { j, js, rc, e };
            if (condition.EmployerIds.Count() > 0)
            {
                query = query.Where(c => condition.EmployerIds.Contains(c.e.Id));
            }
            if (condition.JobStatusIds.Count() > 0)
            {
                query = query.Where(c => condition.JobStatusIds.Contains(c.js.Id));
            }
            int total = await query.CountAsync();
            return total;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:23/8/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<List<CandidateApplyJobAggregate>> GetCandidateApplyJobByJobIdAsync(long jobId)
        {
            var query =
                from caj in _db.CandidateApplyJobs
                join job in _db.Jobs on caj.JobId equals job.Id
                join ccv in _db.CandidateCVPDFs on caj.CandidateCVPDFId equals ccv.Id
                join c in _db.Candidates on ccv.CandidateId equals c.Id
                join cs in _db.CandidateApplyJobStatuses on caj.CandidateApplyJobStatusId equals cs.Id
                where caj.JobId == jobId && caj.Active && job.Active && ccv.Active && c.Active && cs.Active
                select new CandidateApplyJobAggregate
                {
                    Id = caj.Id,
                    JobId = job.Id,
                    IsEmployerViewed = caj.IsEmployerViewed,
                    CandidateId = c.Id,
                    CandidateName = c.FullName,
                    CandidateApplyJobStatusId = caj.CandidateApplyJobStatusId,
                    CandidateApplyJobStatusName = cs.Name,
                    CandidateApplyJobStatusColor = cs.Color,
                    CandidateCVPDFUrl = ccv.Url,
                    CreatedTime = caj.CreatedTime,
                };

            return await query.ToListAsync();
        }

        public async Task<List<JobDetailAggregates>> GetJobDetailByJobIdAsync(long jobId)
        {
            var query = from j in _db.Jobs
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join js in _db.JobStatuses on j.JobStatusId equals js.Id
                        join jc in _db.JobCategories on j.PrimaryJobCategoryId equals jc.Id
                        join jt in _db.JobTypes on j.JobTypeId equals jt.Id
                        join jp in _db.JobPosition on j.JobPositionId equals jp.Id
                        //join jsjp in _db.JobSecondaryJobPositions on jp.Id equals jsjp.JobPositionId
                        where j.Active && rc.Active && js.Active && jc.Active && jt.Active
                        select new JobDetailAggregates
                        {
                            Id = j.Id,
                            Name = j.Name,
                            RecruimentCampaignId = j.RecruimentCampaignId,
                            RecruimentCampaignName = rc.Name,
                            JobStatusId = j.JobStatusId,
                            JobStatusName = js.Name,
                            PrimaryJobCategoryId = j.PrimaryJobCategoryId,
                            PrimaryJobCategoryName = jc.Name,
                            JobTypeId = j.JobTypeId,
                            JobTypeName = jt.Name,
                            Description = j.Description,
                            TotalRecruitment = j.TotalRecruitment,
                            JobPositionId = j.JobPositionId,
                            JobPositionName = jp.Name,
                            Requirement = j.Requirement,
                            Benefit = j.Benefit,
                            ReceiverEmail = j.ReceiverEmail,
                            ReceiverPhone = j.ReceiverPhone,
                            ReceiverName = j.ReceiverName,
                            ApplyEndDate = j.ApplyEndDate,
                            ListJobSecondaryJobCategory = (
                                                         from jsjp in _db.JobSecondaryJobCategories
                                                         where jsjp.JobCategoryId == jc.Id && jsjp.Active
                                                         select (int)jsjp.Id
                                                         ).ToList(),
                            ListJobRequireWorkplace = (
                                                        from jrwp in _db.JobRequireCities
                                                        where jrwp.CityId == j.Id && jrwp.Active
                                                        select new JobWorkPlaceAggregates
                                                        {
                                                            CityId = jrwp.CityId
                                                        }).ToList(),
                            ListJobRequireSkill = (
                                                        from jrs in _db.JobRequireJobSkills
                                                        where jrs.JobSkillId == j.Id && jrs.Active
                                                        select (int)jrs.Id
                                                        ).ToList(),
                            ListTag = (
                        from t in _db.Tags
                        where t.TagTypeId == TagTypeId.JOB && t.Active
                        select t.Id
                    ).ToList(),
                        };
            var result = await query.ToListAsync();
            return result;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:28/8/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<RecruitmentCampaign> GetRecruitmentCampaignByJobIdAsync(long jobId)
        {
            var job = await _db.Jobs
                .Where(j => j.Active)
                .Include(j => j.RecruitmentCampaign)
                .FirstOrDefaultAsync(j => j.Id == jobId);

            return job.RecruitmentCampaign;
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="recruitmentCampaignId"></param>
        /// <returns></returns>
        public async Task<Company> GetCompanyByRecruitmentCampaignIdAsync(long recruitmentCampaignId)
        {
            var company = await _db.RecruitmentCampaigns
                .Where(rc => rc.Id == recruitmentCampaignId)
                .Select(rc => rc.Employer.Company)
                .FirstOrDefaultAsync();

            return company;
        }


        /// <summary>
        /// Author: Nam Anh
        /// Created:28/8/2023
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WorkPlace>> GetWorkPlacesByJobIdAsync(long jobId)
        {
            return await _db.JobRequireCities
                            .Where(j => j.JobId == jobId && j.Active)
                            .Select(j => j.WorkPlace)
                            .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:28/8/2023
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Job>> GetAllPrimaryJobCategoriesAsync()
        {
            return await _db.Jobs
                .Where(j => j.Active)
                .Include(j => j.PrimaryJobCategory)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<List<JobSecondaryJobCategory>> GetSecondaryJobCategoriesByJobIdAsync(long jobId)
        {
            var secondaryJobPositions = await _db.JobSecondaryJobCategories
                .Where(jsp => jsp.JobId == jobId && jsp.Active)
                .Include(jsp => jsp.JobCategory) // Bao gồm thông tin về JobPosition
                .ToListAsync();

            return secondaryJobPositions;
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<List<int>> GetTagIdsByJobIdAsync(long jobId)
        {
            return await _db.JobTags
                                  .Where(jt => jt.JobId == jobId && jt.Active)
                                  .Select(jt => jt.TagId)
                                  .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<List<int>> GetJobSkillIdsByJobIdAsync(long jobId)
        {
            return await _db.JobRequireJobSkills
                                  .Where(jt => jt.JobId == jobId && jt.Active)
                                  .Select(jt => jt.JobSkillId)
                                  .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <returns></returns>
        public async Task<List<JobCategory>> GetSecondaryJobCategoriesForSelect()
        {
            return await _db.JobSecondaryJobCategories
                .Where(jsp => jsp.Active)
                .Select(jsp => jsp.JobCategory)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tag>> GetJobTagsAsync()
        {
            return await _db.Tags.Where(c => c.Active && c.TagTypeId == TagTypeId.JOB)
                            .ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// </summary>
        /// <returns></returns>
        public async Task<List<JobSkill>> GetJobSkillsAsync()
        {
            return await _db.JobRequireJobSkills
                            .Where(c => c.Active)
                            .Select(c => c.JobSkill)
                            .Distinct()
                            .ToListAsync();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/09/2023
        /// Description: Check privacy
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="userId">Employer logged Id</param>
        /// <returns></returns>
        public async Task<bool> Privacy(long id, long userId)
        {
            var query = from j in _db.Jobs
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        where j.Active && rc.Active && e.Active && j.Id == id && e.Id == userId
                        select j;
            bool result = await query.AnyAsync();
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get list job suggesstion 
        /// </summary>
        /// <returns></returns>
        public async Task<List<JobSuggestionAggregate>> ListSuggestion()
        {
            var query = from j in _db.Jobs
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        join c in _db.Companies on e.Id equals c.EmployerId
                        where j.Active && j.IsApproved && rc.Active && rc.IsAprroved && e.Active && c.Active
                        select new JobSuggestionAggregate()
                        {
                            CompanyPhoto = c.Logo,
                            Id = j.Id,
                            Name = j.Name
                        };
            var result = await query.OrderBy(c => Guid.NewGuid()).Take(JobConst.TOTAl_SUGGESTION_JOB_SEARCH_IN_HOME_PAGE).ToListAsync();
            return result;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: serach job suggestion by keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<JobSuggestionAggregate>> SearchSuggestion(string keyword)
        {
            var query = from j in _db.Jobs
                        join rc in _db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join e in _db.Employers on rc.EmployerId equals e.Id
                        join c in _db.Companies on e.Id equals c.EmployerId
                        where j.Active && j.IsApproved && rc.Active && rc.IsAprroved && e.Active && c.Active && EF.Functions.Collate(j.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                        orderby j.CreatedTime descending
                        select new JobSuggestionAggregate()
                        {
                            CompanyPhoto = c.Logo,
                            Id = j.Id,
                            Name = j.Name
                        };
            var result = await query.Take(JobConst.TOTAl_SUGGESTION_JOB_SEARCH_IN_HOME_PAGE).ToListAsync();
            return result;
        }
    }
}
