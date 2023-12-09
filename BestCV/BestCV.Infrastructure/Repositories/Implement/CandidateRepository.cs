using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Candidate;
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
    public class CandidateRepository : RepositoryBaseAsync<Candidate, long, JobiContext>, ICandidateRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime: 26/07/2023
        /// /// Description: Check candidate email is Existed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailExist(string email)
        {
            var data = await db.Candidates.AnyAsync(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
            return data;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime: 26/07/2023
        /// Description: Check candidate phone is Existed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> IsPhoneExist(string phone)
        {
            var data = await db.Candidates.AnyAsync(x => x.Phone == phone);
            return data;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime: 26/07/2023
        /// Description: Check candidate username is Existed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsFullNameExist(string fullName)
        {
            var data = await db.Candidates.AnyAsync(x => x.FullName.ToLower().Trim() == fullName.ToLower().Trim());
            return data;
        }

        public async Task<Candidate> FindByEmail(string email)
        {
            var data = (await FindByConditionAsync(x => x.Email == email)).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:26/7/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Candidate> SignIn(Candidate obj)
        {
            var data = await (from c in db.Candidates
                              where (c.Active && (c.Phone == obj.Phone || c.Email == obj.Email))
                              select c)
                             .FirstOrDefaultAsync();
            return data;
        }

        /// <summary>
        /// Author: Nam Anh
        /// CreatedDate: 01/08/2023
        /// Description: Lấy thông tin ứng viên theo Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Thông tin ứng viên</returns>
        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            return await db.Candidates.Where(e => e.Email.Equals(email) && e.Active).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// CreatedDate: 01/08/2023
        /// Description: Lấy thông tin ứng viên theo phone
        /// </summary>
        /// <param name="phone">phone</param>
        /// <returns>Thông tin ứng viên</returns>
        public async Task<Candidate?> GetByPhoneAsync(string phone)
        {
            return await db.Candidates.Where(e => e.Phone.Equals(phone) && e.Active).FirstOrDefaultAsync();
        }

        public Task UpdateGoogleId(Candidate obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.GoogleId).IsModified = true;
            db.Entry(obj).Property(c => c.Password).IsModified = false;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Check candidate FacebookId
        /// </summary>
        /// <param name="facebookId"></param>
        /// <returns></returns>
        public async Task<Candidate> CheckCandidateByFacebookId(string facebookId)
        {
            return await db.Candidates.Where(x => x.Active && x.FacebookId == facebookId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : update FacebookId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task UpdateFacebookId(Candidate obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.FacebookId).IsModified = true;
            db.Entry(obj).Property(c => c.Password).IsModified = false;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 03/08/2023
        /// Description : Check candidate LinkedinId
        /// </summary>
        /// <param name="linkedinId"></param>
        /// <returns></returns>
        public async Task<Candidate> CheckCandidateByLinkedinId(string linkedinId)
        {
            return await db.Candidates.Where(x => x.Active && x.LinkedinId == linkedinId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 03/08/2023
        /// Description : update LinkedinId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task UpdateLinkedinId(Candidate obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.LinkedinId).IsModified = true;
            db.Entry(obj).Property(c => c.Password).IsModified = false;

            return Task.CompletedTask;
        }
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime: 02/08/2023
        /// Description : LoadData Candidate Aggregates (admin page)
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ListCandidateAggregates(CandidateDTParameters parameters)
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
                from c in db.Candidates
                join cs in db.AccountStatuses on c.CandidateStatusId equals cs.Id
                join cl in db.CandidateLevels on c.CandidateLevelId equals cl.Id


                where c.Active && cs.Active && cl.Active
                select new CandidateAggregates
                {
                    Id = c.Id,
                    CandidateLevelId = c.CandidateLevelId,
                    CandidateLevelName = cl.Name,
                    CandidateStatusId = c.CandidateStatusId,
                    CandidateStatusName = cs.Name,
                    Active = c.Active,
                    AccessFailedCount = c.AccessFailedCount,
                    AddressDetail = c.AddressDetail,
                    CandidateLevelEfficiencyExpiry = c.CandidateLevelEfficiencyExpiry,
                    CandidateStatusColor = cs.Color,
                    CoverPhoto = c.CoverPhoto,
                    CreatedTime = c.CreatedTime,
                    DoB = c.DoB,
                    Email = c.Email,
                    FacebookId = c.FacebookId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FullName = c.FullName,
                    Gender = c.Gender,
                    GoogleId = c.GoogleId,
                    Info = c.Info,
                    Interests = c.Interests,
                    IsActivated = c.IsActivated,
                    IsCheckJobOffers = c.IsCheckJobOffers,
                    IsCheckOnJobWatting = c.IsCheckOnJobWatting,
                    IsCheckTopCVReview = c.IsCheckTopCVReview,
                    IsCheckViewCV = c.IsCheckViewCV,
                    IsSubcribeEmailEmployerInviteJob = c.IsSubcribeEmailEmployerInviteJob,
                    IsSubcribeEmailEmployerViewCV = c.IsSubcribeEmailEmployerViewCV,
                    IsSubcribeEmailGiftCoupon = c.IsSubcribeEmailGiftCoupon,
                    IsSubcribeEmailImportantSystemUpdate = c.IsSubcribeEmailImportantSystemUpdate,
                    IsSubcribeEmailJobSuggestion = c.IsSubcribeEmailJobSuggestion,
                    IsSubcribeEmailNewFeatureUpdate = c.IsSubcribeEmailNewFeatureUpdate,
                    IsSubcribeEmailOtherSystemNotification = c.IsSubcribeEmailOtherSystemNotification,
                    IsSubcribeEmailProgramEventIntro = c.IsSubcribeEmailProgramEventIntro,
                    IsSubcribeEmailServiceIntro = c.IsSubcribeEmailServiceIntro,
                    JobPosition = c.JobPosition,
                    LinkedinId = c.LinkedinId,
                    LockEnabled = c.LockEnabled,
                    LockEndDate = c.LockEndDate,
                    MiddleName = c.MiddleName,
                    Objective = c.Objective,
                    Password = c.Password,
                    Phone = c.Phone,
                    Photo = c.Photo,
                    Search = c.Search,
                    SuggestionExperienceRangeId = c.SuggestionExperienceRangeId,
                    SuggestionSalaryRangeId = c.SuggestionSalaryRangeId,
                    Username = c.Username,
                    ListCandidateCertificate = db.CandidateCertificates.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.Name).ToList(),
                    ListCandidateEducation = db.CandidateEducations.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.School + " " + x.Title).ToList(),
                    ListCandidateSuggestionJobCategory = db.CandidateSuggestionJobCategories.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.JobCategoryId).ToList(),
                    ListCandidateSuggestionJobJobSkill = db.CandidateSuggestionJobSkills.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.JobSkillId).ToList(),
                    ListCandidateSuggestionJobPosition = db.CandidateSuggestionJobPositions.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.JobPositionId).ToList(),
                    ListCandidateSuggestionJobWorkplace = db.CandidateSuggestionWorkPlaces.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.WorkPlaceId).ToList(),
                    ListCandidateSkill = db.CandidateSkills.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.Name).ToList(),
                    ListCandidateSkillLevel = db.CandidateSkills.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.SkillLevelId).ToList(),

                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword) || s.FullName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.Username.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.CandidateLevelName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.CreatedTime.ToCustomString().Contains(keyword) || s.CandidateStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))).ToList();

            //advanced search category
            if (parameters.SearchCategory != "")
            {
                var arrCategory = parameters.SearchCategory.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobCategory.Any(c => arrCategory.Contains(c))).ToList();
            }

            //advanced search job skill
            if (parameters.SearchSuggestSkill != "")
            {
                var arrSkill = parameters.SearchSuggestSkill.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobJobSkill.Any(c => arrSkill.Contains(c))).ToList();
            }

            //advanced search position
            if (parameters.SearchPosition != "")
            {
                var arrPosition = parameters.SearchPosition.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobPosition.Any(c => arrPosition.Contains(c))).ToList();
            }

            //advanced search work place
            if (parameters.SearchCity != "")
            {
                var arrWorkplace = parameters.SearchCity.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobWorkplace.Any(c => arrWorkplace.Contains(c))).ToList();
            }

            //advanced search salary
            if (parameters.SearchSalaryRange != "")
            {
                var arrSalary = parameters.SearchSalaryRange.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => arrSalary.Contains(x.SuggestionSalaryRangeId)).ToList();
            }

            //advanced search experience
            if (parameters.SearchExperience != "")
            {
                var arrExperience = parameters.SearchExperience.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => arrExperience.Contains(x.SuggestionExperienceRangeId)).ToList();
            }

            //advanced search education
            if (parameters.SearchEducation != "")
            {

                result = result.Where(x => x.ListCandidateEducation.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchEducation.ToLower().RemoveVietnamese()))).ToList();
            }

            //advanced search certificate
            if (parameters.SearchCertificate != "")
            {
                result = result.Where(x => x.ListCandidateCertificate.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCertificate.ToLower().RemoveVietnamese()))).ToList();
            }


            //advanced search candidate skill
            if (parameters.SearchCandidateSkill != "")
            {
                if (parameters.SearchCandidateSkillLevel != "")
                {
                    var arrSkillLevel = parameters.SearchCandidateSkillLevel.Split(',').Select(x => Int32.Parse(x));
                    result = result.Where(x => x.ListCandidateSkill.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCandidateSkill.ToLower().RemoveVietnamese())) && x.ListCandidateSkillLevel.Any(c => arrSkillLevel.Contains(c))).ToList();
                }
                else
                {
                    result = result.Where(x => x.ListCandidateSkill.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCandidateSkill.ToLower().RemoveVietnamese()))).ToList();
                }
            }

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
                    case "fullName":
                        result = result.Where(r => r.FullName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "username":
                        result = result.Where(r => r.Username.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "candidateLevelName":
                        var candiDateLevelIdsArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => candiDateLevelIdsArr.Contains(r.CandidateLevelId)).ToList();
                        break;
                    case "candidateStatusName":
                        var candidateStatusIdsArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => candidateStatusIdsArr.Contains(r.CandidateStatusId)).ToList();
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
                    case "isActivated":
                        if (search != "null")
                        {
                            result = result.Where(r => r.IsActivated.ToString().ToLower().Contains(search)).ToList();
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
        /// <summary>
        /// Author : ThanhND 
        /// CreatedTime : 03/08
        /// </summary>
        /// <param name="id">candidateId</param>
        /// <returns></returns>
        public async Task<bool> QuickActivatedAsync(long id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.IsActivated = obj.IsActivated ? false : true;

                db.Attach(obj);
                db.Entry(obj).Property(x => x.IsActivated).IsModified = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime: 03/08/2023
        /// Description: Change password for admin page
        /// </summary>
        /// <param name="obj">Candidate</param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(Candidate obj)
        {
            if (obj != null)
            {
                db.Attach(obj);
                db.Entry(obj).Property(x => x.Password).IsModified = true;
                return true;
            }
            return false;
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await db.Candidates.AnyAsync(e => e.Email.Equals(email) && e.Active);
        }


        /// <summary>
        /// Author : ThanhND
        /// CreatedTime: 25/09/2023
        /// Description : Find Candidate Aggregates (employer page)
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> FindCandidateAgrregates(FindCandidateParameters parameters)
        {
            //tim thong tin cty
            var companyId = await db.Companies.Where(x => x.Active && x.EmployerId == parameters.EmployerId).Select(x => x.Id).FirstOrDefaultAsync();
            var creditWallet = await db.EmployerWallets.Where(x => x.Active && x.EmployerId == parameters.EmployerId && x.WalletTypeId == EmployerWalletConstants.CREDIT_TYPE).FirstOrDefaultAsync();

            var typeOfEmployer = 1;
            //tim thong tin ntd
            if (companyId != 0)
            {
                var fieldOfActivity = await db.CompanyFieldOfActivities.Where(x => x.Active && x.CompanyId == companyId).Select(x => x.Id).ToListAsync();
                if (!fieldOfActivity.Contains(FieldOfActivityConst.JOB_SERVICE))
                {
                    typeOfEmployer = 1;
                }
                else
                {
                    typeOfEmployer = 2;
                }
            }

            //khai báo biến
            var keyword = parameters.Search?.Value;
            var filterCV = parameters.FilterCV;

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
                from c in db.Candidates
                join cs in db.AccountStatuses on c.CandidateStatusId equals cs.Id
                join cl in db.CandidateLevels on c.CandidateLevelId equals cl.Id
                join ex in db.ExperienceRange on c.SuggestionExperienceRangeId equals ex.Id
                join sr in db.SalaryRange on c.SuggestionSalaryRangeId equals sr.Id



                let listCandidateSkillObject = (from ck in db.CandidateSkills
                                                join ckl in db.SkillLevels on ck.SkillLevelId equals ckl.Id
                                                where ck.Active && ckl.Active && ck.CandidateId == c.Id
                                                && ck.SkillId == SkillConst.JAPANESE_SKILL
                                                && (ck.SkillLevelId == SkillLevelConstants.MEDIUM
                                                || ck.SkillLevelId == SkillLevelConstants.GOOD
                                                || ck.SkillLevelId == SkillLevelConstants.EXELLENT)
                                                select ck).ToList()

                where c.Active && cs.Active && cl.Active && ex.Active && sr.Active
                select new CandidateAggregates
                {
                    Id = c.Id,
                    CandidateLevelId = c.CandidateLevelId,
                    CandidateLevelName = cl.Name,
                    CandidateStatusId = c.CandidateStatusId,
                    CandidateStatusName = cs.Name,
                    Active = c.Active,
                    AccessFailedCount = c.AccessFailedCount,
                    AddressDetail = c.AddressDetail,
                    CandidateLevelEfficiencyExpiry = c.CandidateLevelEfficiencyExpiry,
                    CandidateStatusColor = cs.Color,
                    CoverPhoto = c.CoverPhoto,
                    CreatedTime = c.CreatedTime,
                    DoB = c.DoB,
                    Email = c.Email,
                    FacebookId = c.FacebookId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FullName = c.FullName,
                    Gender = c.Gender,
                    GoogleId = c.GoogleId,
                    Info = c.Info,
                    Interests = c.Interests,
                    IsActivated = c.IsActivated,
                    IsCheckJobOffers = c.IsCheckJobOffers,
                    IsCheckOnJobWatting = c.IsCheckOnJobWatting,
                    IsCheckTopCVReview = c.IsCheckTopCVReview,
                    IsCheckViewCV = c.IsCheckViewCV,
                    IsSubcribeEmailEmployerInviteJob = c.IsSubcribeEmailEmployerInviteJob,
                    IsSubcribeEmailEmployerViewCV = c.IsSubcribeEmailEmployerViewCV,
                    IsSubcribeEmailGiftCoupon = c.IsSubcribeEmailGiftCoupon,
                    IsSubcribeEmailImportantSystemUpdate = c.IsSubcribeEmailImportantSystemUpdate,
                    IsSubcribeEmailJobSuggestion = c.IsSubcribeEmailJobSuggestion,
                    IsSubcribeEmailNewFeatureUpdate = c.IsSubcribeEmailNewFeatureUpdate,
                    IsSubcribeEmailOtherSystemNotification = c.IsSubcribeEmailOtherSystemNotification,
                    IsSubcribeEmailProgramEventIntro = c.IsSubcribeEmailProgramEventIntro,
                    IsSubcribeEmailServiceIntro = c.IsSubcribeEmailServiceIntro,
                    JobPosition = c.JobPosition,
                    LinkedinId = c.LinkedinId,
                    LockEnabled = c.LockEnabled,
                    LockEndDate = c.LockEndDate,
                    MiddleName = c.MiddleName,
                    Objective = c.Objective,
                    Phone = c.Phone,
                    Photo = c.Photo,
                    Search = c.Search,
                    SuggestionExperienceRangeId = c.SuggestionExperienceRangeId,
                    SuggestionSalaryRangeId = c.SuggestionSalaryRangeId,
                    Username = c.Username,
                    ListCandidateCertificate = db.CandidateCertificates.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.Name + " - " + x.IssueBy + " (" + x.TimePeriod + ")").ToList(),
                    ListCandidateEducation = db.CandidateEducations.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.School + " " + x.Title).ToList(),
                    ListCandidateSuggestionJobCategory = db.CandidateSuggestionJobCategories.Where(x => x.Active && x.CandidateId == c.Id && x.JobCategory.Active).Select(x => x.JobCategoryId).ToList(),

                    ListCandidateSuggestionJobJobSkill = db.CandidateSuggestionJobSkills.Where(x => x.Active && x.CandidateId == c.Id && x.JobSkill.Active).Select(x => x.JobSkillId).ToList(),
                    ListCandidateSuggestionJobPosition = db.CandidateSuggestionJobPositions.Where(x => x.Active && x.CandidateId == c.Id && x.JobPosition.Active).Select(x => x.JobPositionId).ToList(),
                    ListCandidateSuggestionJobWorkplace = db.CandidateSuggestionWorkPlaces.Where(x => x.Active && x.CandidateId == c.Id && x.WorkPlace.Active).Select(x => x.WorkPlaceId).ToList(),
                    ListCandidateSkill = db.CandidateSkills.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.Name).ToList(),
                    ListCandidateSkillLevel = db.CandidateSkills.Where(x => x.Active && x.CandidateId == c.Id).Select(x => x.SkillLevelId).ToList(),
                    MaritalStatus = c.MaritalStatus,
                    Nationality = c.Nationality,
                    References = c.References,
                    SuggestionExperienceRangeName = db.ExperienceRange.Where(x => x.Id == c.SuggestionExperienceRangeId && x.Active).Select(x => x.Name).FirstOrDefault(),
                    SuggestionSalaryRangeName = db.SalaryRange.Where(x => x.Id == c.SuggestionSalaryRangeId && x.Active).Select(x => x.Name).FirstOrDefault(),
                    ListCandidateSkillObject = listCandidateSkillObject,
                    CountCredit = typeOfEmployer == 3 ? 10 : (typeOfEmployer == 2 && listCandidateSkillObject.Count != 0 ? 10 : (typeOfEmployer == 2 &&
                    c.SuggestionExperienceRangeId == ExperienceRangeConst.TREN_10_NAM ? 20 : (typeOfEmployer == 2 &&
                    c.SuggestionExperienceRangeId == ExperienceRangeConst.TU_5_DEN_10_NAM ? 10 : (typeOfEmployer == 2 &&
                    (c.SuggestionExperienceRangeId == ExperienceRangeConst.TU_1_DEN_2_NAM
                    || c.SuggestionExperienceRangeId == ExperienceRangeConst.DUOI_1_NAM
                    || c.SuggestionExperienceRangeId == ExperienceRangeConst.CHUA_CO_KINH_NGHIEM) ? 5 : (typeOfEmployer == 1 && listCandidateSkillObject.Count != 0 ? 10 : (typeOfEmployer == 1 && c.SuggestionExperienceRangeId == ExperienceRangeConst.TREN_10_NAM ? 3 : (typeOfEmployer == 1 && (c.SuggestionExperienceRangeId == ExperienceRangeConst.TU_5_DEN_10_NAM
                    || c.SuggestionExperienceRangeId == ExperienceRangeConst.TU_3_DEN_5_NAM
                    || c.SuggestionExperienceRangeId == ExperienceRangeConst.TU_1_DEN_2_NAM) ? 2 : 1))))))),
                    IsUsedCredit = db.EmployerWalletHistories.Any(x => x.Active && (x.WalletHistoryTypeId == WalletHistoryTypeConst.SU_DUNG_CREDIT || x.WalletHistoryTypeId == WalletHistoryTypeConst.YEU_CAU_HOAN_CREDIT) && x.CandidateId == c.Id && x.EmployerWalletId == creditWallet.Id),
                    IsRefundRequest = db.EmployerWalletHistories.Any(x => x.Active && x.WalletHistoryTypeId == WalletHistoryTypeConst.YEU_CAU_HOAN_CREDIT && x.CandidateId == c.Id && x.EmployerWalletId == creditWallet.Id)

                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword) 
            || s.FullName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) 
            || s.AddressDetail.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.MaritalStatus.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.SuggestionExperienceRangeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.JobPosition.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || (s.DoB == null ? false : s.DoB.Value.ToString("dd/MM/yyyy").Contains(keyword))
            || s.SuggestionSalaryRangeName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.CandidateLevelName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) 
            || s.CandidateStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) 
            || (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
            ).ToList();

            //advanced search category
            if (parameters.SearchCategory != "")
            {
                var arrCategory = parameters.SearchCategory.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobCategory.Any(c => arrCategory.Contains(c))).ToList();
            }

            //advanced search job skill
            if (parameters.SearchSuggestSkill != "")
            {
                var arrSkill = parameters.SearchSuggestSkill.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobJobSkill.Any(c => arrSkill.Contains(c))).ToList();
            }

            //advanced search position
            if (parameters.SearchPosition != "")
            {
                var arrPosition = parameters.SearchPosition.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobPosition.Any(c => arrPosition.Contains(c))).ToList();
            }

            //advanced search work place
            if (parameters.SearchCity != "")
            {
                var arrWorkplace = parameters.SearchCity.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => x.ListCandidateSuggestionJobWorkplace.Any(c => arrWorkplace.Contains(c))).ToList();
            }

            //advanced search salary
            if (parameters.SearchSalaryRange != "")
            {
                var arrSalary = parameters.SearchSalaryRange.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => arrSalary.Contains(x.SuggestionSalaryRangeId)).ToList();
            }

            //advanced search experience
            if (parameters.SearchExperience != "")
            {
                var arrExperience = parameters.SearchExperience.Split(',').Select(x => Int32.Parse(x));
                result = result.Where(x => arrExperience.Contains(x.SuggestionExperienceRangeId)).ToList();
            }

            //advanced search education
            if (parameters.SearchEducation != "")
            {

                result = result.Where(x => x.ListCandidateEducation.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchEducation.ToLower().RemoveVietnamese()))).ToList();
            }

            //advanced search certificate
            if (parameters.SearchCertificate != "")
            {
                result = result.Where(x => x.ListCandidateCertificate.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCertificate.ToLower().RemoveVietnamese()))).ToList();
            }


            //advanced search candidate skill
            if (parameters.SearchCandidateSkill != "")
            {
                if (parameters.SearchCandidateSkillLevel != "")
                {
                    var arrSkillLevel = parameters.SearchCandidateSkillLevel.Split(',').Select(x => Int32.Parse(x));
                    result = result.Where(x => x.ListCandidateSkill.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCandidateSkill.ToLower().RemoveVietnamese())) && x.ListCandidateSkillLevel.Any(c => arrSkillLevel.Contains(c))).ToList();
                }
                else
                {
                    result = result.Where(x => x.ListCandidateSkill.Any(c => c.RemoveVietnamese().ToLower().Contains(parameters.SearchCandidateSkill.ToLower().RemoveVietnamese()))).ToList();
                }
            }

            //filter cv
            if (!string.IsNullOrEmpty(filterCV))
            {
                switch (filterCV)
                {
                    case "all":
                        break;
                    case "unread":
                        result = result.Where(x => !x.IsUsedCredit).ToList();
                        break;
                    case "read":
                        result = result.Where(x => x.IsUsedCredit).ToList();
                        break;
                    case "refund":
                        result = result.Where(x => x.IsRefundRequest).ToList();
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
    }
}
