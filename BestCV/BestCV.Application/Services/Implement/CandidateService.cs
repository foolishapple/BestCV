using AutoMapper;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Routing.Matching;
using BestCV.Application.Models.CandidateWorkExperience;
using BestCV.Application.Models.CandidateSkill;
using BestCV.Application.Models.CandidateEducation;
using BestCV.Application.Models.CandidateActivites;
using BestCV.Application.Models.CandidateCertificate;
using BestCV.Application.Models.CandidateHonorAward;
using BestCV.Application.Models.CandidateProjects;
using BestCV.Domain.Aggregates.Candidate;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using Microsoft.AspNetCore.Mvc;
using BestCV.Application.Models.CandidateJobSuggestionSetting;
using BestCV.Application.Models.CandidateSuggestionJobCategory;
using BestCV.Application.Models.CandidateSuggestionJobPosition;
using BestCV.Application.Models.CandidateSuggestionJobSkill;
using BestCV.Application.Models.CandidateSuggestionWorkPlace;

namespace BestCV.Application.Services.Implement
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository repository;
        private readonly ICandidateMetaRepository metaRepository;
        private readonly ICandidateSuggestionJobCategoryRepository candidateSuggestionJobCategoryRepository;
        private readonly ICandidateSuggestionJobPositionRepository candidateSuggestionJobPositionRepository;
        private readonly ICandidateSuggestionJobSkillRepository candidateSuggestionJobSkillRepository;
        private readonly ICandidateSuggestionWorkPlaceRepository candidateSuggestionWorkPlaceRepository;
        private readonly ICandidateSkillRepository candidateSkillRepository;
        private readonly ICandidateWorkExperienceRepository candidateWorkExperienceRepository;
        private readonly ICandidateEducationRepository candidateEducationRepository;
        private readonly ICandidateActivitesRepository candidateActivitesRepository;
        private readonly ICandidateCertificateRepository candidateCertificateRepository;
        private readonly ICandidateHonorAwardRepository candidateHonorAwardRepository;
        private readonly ICandidateProjectsRepository candidateProjectsRepository;
        private readonly ILogger<CandidateService> logger;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private static IDictionary<string, byte[]> _files = new Dictionary<string, byte[]>();

        public CandidateService(ICandidateRepository _repository, ICandidateMetaRepository _metaRepository, ICandidateSuggestionWorkPlaceRepository _candidateSuggestionWorkPlaceRepository, ICandidateSuggestionJobSkillRepository _candidateSuggestionJobSkillRepository, ICandidateSuggestionJobPositionRepository _candidateSuggestionJobPositionRepository, ICandidateSuggestionJobCategoryRepository _candidateSuggestionJobCategoryRepository, ILoggerFactory loggerFactory, IEmailService _emailService, IConfiguration _configuration, IMapper _mapper, ICandidateSkillRepository _candidateSkillRepository, ICandidateWorkExperienceRepository _candidateWorkExperienceRepository, ICandidateEducationRepository _candidateEducationRepository, ITokenService _tokenService, ICandidateHonorAwardRepository _candidateHonorAwardRepository, ICandidateActivitesRepository _candidateActivitesRepository, ICandidateCertificateRepository _candidateCertificateRepository, ICandidateProjectsRepository _candidateProjectsRepository)
        {
            repository = _repository;
            metaRepository = _metaRepository;
            logger = loggerFactory.CreateLogger<CandidateService>();
            emailService = _emailService;
            configuration = _configuration;
            tokenService = _tokenService;
            mapper = _mapper;         
            candidateSuggestionJobCategoryRepository = _candidateSuggestionJobCategoryRepository;
            candidateSuggestionJobPositionRepository = _candidateSuggestionJobPositionRepository;
            candidateSuggestionJobSkillRepository = _candidateSuggestionJobSkillRepository;
            candidateSuggestionWorkPlaceRepository = _candidateSuggestionWorkPlaceRepository;
            candidateSkillRepository = _candidateSkillRepository;
            candidateWorkExperienceRepository = _candidateWorkExperienceRepository;
            candidateEducationRepository = _candidateEducationRepository;
            candidateActivitesRepository = _candidateActivitesRepository;
            candidateCertificateRepository = _candidateCertificateRepository;
            candidateHonorAwardRepository = _candidateHonorAwardRepository;
            candidateProjectsRepository = _candidateProjectsRepository;
        }

        public async Task<(BestCVResponse, EmailMessage<CandidateConfirmEmailBody>?)> CandidateSignup(SignupCandidateDTO signupCandidateDTO)
        {
            var checkEmail = await repository.IsEmailExist(signupCandidateDTO.Email);
            var checkPhone = await repository.IsPhoneExist(signupCandidateDTO.Phone);
            if (checkEmail)
            {
                return (BestCVResponse.Error("Email đã được sử dụng."), null);
            } else if (checkPhone)
            {
                return (BestCVResponse.Error("Số điện thoại đã được sử dụng."), null);
            }
            else
            {
                var user = mapper.Map<Candidate>(signupCandidateDTO);

                user.CandidateStatusId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_STATUS;
                user.CandidateLevelId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_LEVEL;
                user.Email = signupCandidateDTO.Email;
                user.Username = signupCandidateDTO.Email;
                user.Active = true;
                user.Password = signupCandidateDTO.Password.ToHash256();
                user.Phone = signupCandidateDTO.Phone;
                user.FullName = signupCandidateDTO.FullName;
                user.CreatedTime = DateTime.Now;
                user.Search = user.Phone + "-" + user.FullName + "-" + user.CreatedTime + "-" + user.Email;
                user.Photo = CandidateConstants.DEFAULT_PHOTO;
                user.CoverPhoto = CandidateConstants.DEFAULT_CONVER_PHOTO;
                user.SuggestionExperienceRangeId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_SUGGESTION_EXPRERIENCE_RANGE;
                user.SuggestionSalaryRangeId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_SUGGESTION_SALARY_RANGE;
                user.IsSubcribeEmailImportantSystemUpdate = true;
                user.IsSubcribeEmailEmployerViewCV = true;
                user.IsSubcribeEmailNewFeatureUpdate = true;
                user.IsSubcribeEmailOtherSystemNotification = true;
                user.IsSubcribeEmailJobSuggestion = true;
                user.IsSubcribeEmailEmployerInviteJob = true;
                user.IsSubcribeEmailServiceIntro = true;
                user.IsSubcribeEmailProgramEventIntro = true;
                user.IsSubcribeEmailGiftCoupon = true;
                user.IsActivated = true;
                var database = await metaRepository.BeginTransactionAsync();
                using (database)
                {
                    try
                    {
                        // insert into candidate table
                        await repository.CreateAsync(user);
                        await repository.SaveChangesAsync();
                        if (user.Id > 0)
                        {
                            var randomString = StringExtension.RandomString(8);
                            var candidateMeta = new CandidateMeta()
                            {
                                Id = 0,
                                CandidateId = user.Id,
                                Key = CandidateConstants.CANDIDATE_META_VERIFY_EMAIL,
                                Value = randomString,
                                Active = true,
                                CreatedTime = DateTime.Now,
                                Name = user.Email,
                                Description = (user.Email + "-" + randomString).ToHash256(),
                            };
                            await metaRepository.CreateAsync(candidateMeta);
                            await metaRepository.SaveChangesAsync();
                            if (candidateMeta.Id > 0)
                            {
                                // Gửi email khi đăng ký thành công
                                var message = SendEmailAsync(user, candidateMeta);

                                logger.LogInformation("Tài khoản {Username} đã được tạo lúc {CreatedTime}", user.FullName, DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));

                                //hoàn thành transaction
                                await metaRepository.EndTransactionAsync();
                                return (BestCVResponse.Success(signupCandidateDTO), message);
                            }
                            else
                            {
                                await metaRepository.RollbackTransactionAsync();
                                return (BestCVResponse.Error("Tạo tài khoản ứng viên không thành công"), null);
                            }
                        }
                        else
                        {
                            await metaRepository.RollbackTransactionAsync();
                            return (BestCVResponse.Error("Tạo tài khoản ứng viên không thành công"), null);
                        }

                    }
                    catch (Exception ex)
                    {

                        logger.LogError(ex, $"Có lỗi khi tạo tài khoản ứng viên{user.Email}");
                        return (BestCVResponse.Error(), null);
                    }
                }
            } 
        }
        private string GenerateVerificationCode()
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateAsync(SignupCandidateDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<SignupCandidateDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {

            var candidateAccount = await repository.GetByIdAsync(id);
            if (candidateAccount == null)
            {
                return BestCVResponse.NotFound("Not found", id);
            }
            var candidateDTO = mapper.Map<CandidateDTO>(candidateAccount);
            var mappingWE = await candidateWorkExperienceRepository.ListCandidateWorkExperienceByCandidateId(id);
            candidateDTO.ListCandidateWorkExperiences = mapper.Map<List<CandidateWorkExperienceDTO>>(mappingWE);
            candidateDTO.ListCandidateSkill = mapper.Map<List<CandidateSkillDTO>>(await candidateSkillRepository.ListCandidateSkillByCandidateId(id));
            candidateDTO.ListCandidateEducation = mapper.Map<List<CandidateEducationDTO>>(await candidateEducationRepository.ListCandidateEducationByCandidateId(id));
            candidateDTO.ListCandidateActivites = mapper.Map<List<CandidateActivitesDTO>>(await candidateActivitesRepository.ListCandidateActivitesByCandidateId(id));
            var certificate = await candidateCertificateRepository.FindByConditionAsync(x => x.Active && x.CandidateId == id);
            candidateDTO.ListCandidateCertificates = mapper.Map<List<CandidateCertificateDTO>>(certificate);
            candidateDTO.ListCandidateHonorAwards = mapper.Map<List<CandidateHonorAwardDTO>>(await candidateHonorAwardRepository.ListCandidateHonorAwardByCandidateId(id));
            candidateDTO.ListCandidateProjects = mapper.Map<List<CandidateProjectsDTO>>(await candidateProjectsRepository.ListCandidateProjectsByCandidateId(id));
            candidateDTO.ListCandidateSuggestionJobCategory = mapper.Map<List<CandidateSuggestionJobCategoryDTO>>(await candidateSuggestionJobCategoryRepository.ListByCandidateIdAsync(id));
            candidateDTO.ListCandidateSuggestionJobPosition = mapper.Map<List<CandidateSuggestionJobPositionDTO>>(await candidateSuggestionJobPositionRepository.ListByCandidateIdAsync(id));
            candidateDTO.ListCandidateSuggestionJobSkill = mapper.Map<List<CandidateSuggestionJobSkillDTO>>(await candidateSuggestionJobSkillRepository.ListByCandidateIdAsync(id));
            candidateDTO.candidateSuggestionWorkPlaces = mapper.Map<List<CandidateSuggestionWorkPlaceDTO>>(await candidateSuggestionWorkPlaceRepository.ListByCandidateIdAsync(id));
            return BestCVResponse.Success(candidateDTO);
        }

      
        public async Task<BestCVResponse> ListCandidateDetailById(long id)
        {

            var candidateAccount = await repository.GetByIdAsync(id);
            if (candidateAccount == null)
            {
                return BestCVResponse.NotFound("Not found", id);
            }
            var candidateDTO = mapper.Map<CandidateDTO>(candidateAccount);
            candidateDTO.ListCandidateWorkExperiences = mapper.Map<List<CandidateWorkExperienceDTO>>(await candidateWorkExperienceRepository.ListCandidateWorkExperienceByCandidateId(id));
            //candidateDTO.ListCandidateSkill = mapper.Map<List<CandidateSkillDTO>>(await candidateSkillRepository.ListCandidateSkillByCandidateId(id));
            candidateDTO.ListCandidateSkill = mapper.Map<List<CandidateSkillDTO>>(await candidateSkillRepository.FindByConditionAsync(x=>x.Active && x.CandidateId == id));
            candidateDTO.ListCandidateEducation = mapper.Map<List<CandidateEducationDTO>>(await candidateEducationRepository.ListCandidateEducationByCandidateId(id));
            candidateDTO.ListCandidateActivites = mapper.Map<List<CandidateActivitesDTO>>(await candidateActivitesRepository.ListCandidateActivitesByCandidateId(id));
            candidateDTO.ListCandidateCertificates = mapper.Map<List<CandidateCertificateDTO>>(await candidateCertificateRepository.ListCandidateCetificateByCandidateId(id));
            candidateDTO.ListCandidateHonorAwards = mapper.Map<List<CandidateHonorAwardDTO>>(await candidateHonorAwardRepository.ListCandidateHonorAwardByCandidateId(id));
            candidateDTO.ListCandidateProjects = mapper.Map<List<CandidateProjectsDTO>>(await candidateProjectsRepository.ListCandidateProjectsByCandidateId(id));
            return BestCVResponse.Success(candidateDTO);


        }
        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(SigninCandidateDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateProfileCandidateDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<SigninCandidateDTO> obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateProfileCandidateDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateNotiEmailCandidate(SettingNotiEmailDTO obj)
        {
            var candidateAccount = await repository.GetByIdAsync(obj.Id);
            if (candidateAccount == null)
            {
                return BestCVResponse.NotFound("Not found", obj);
            }
            var updateAccount = mapper.Map(obj, candidateAccount);
            await repository.UpdateAsync(updateAccount);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(updateAccount);
        }


        public async Task<BestCVResponse> UpdatePasswordCandidate(ChangePasswordDTO obj, long userId)
        {
            var candidateAccount = await repository.GetByIdAsync(userId);
            if (candidateAccount == null)
            {
                return BestCVResponse.NotFound($"Không tìm thấy ID tài khoản ứng viên: {userId}", obj);
            }
            if (candidateAccount.Password != obj.OldPassword.ToHash256())
            {
                return BestCVResponse.BadRequest(new List<string>() { "Mật khẩu cũ không chính xác." });
            }
            candidateAccount.Password = obj.NewPassword.ToHash256();
            await repository.UpdateAsync(candidateAccount);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(candidateAccount);
        }


        public async Task<BestCVResponse> CheckVerifyCode(string value, string hashCode)
        {
            var dataMeta = await metaRepository.CheckVerifyCode(value, hashCode);
            if (dataMeta != null)
            {
                if (DateTime.Now > dataMeta.CreatedTime.AddHours(CandidateConstants.PASSSWORD_SENT_EMAIL_TIME_LIMIT))
                {
                    return BestCVResponse.Error();
                }
                else
                {
                    dataMeta.Active = false;

                    await metaRepository.UpdateAsync(dataMeta);
                    await metaRepository.SaveChangesAsync();
                    return BestCVResponse.Success(dataMeta);
                }
            }
            return BestCVResponse.NotFound("", dataMeta);
        }

 
        public async Task ActivateAccount(string email)
        {
            var data = await repository.FindByEmail(email);
            data.IsActivated = true;
            await repository.UpdateAsync(data);
            await repository.SaveChangesAsync();
        }

        public async Task<int> CountSendingEmail(long id)
        {
            return await metaRepository.CountSendingEmail(id);
        }


        public async Task<CandidateMeta> GetEmailByVerifyCode(ConfirmEmailCandidateDTO obj)
        {
            var data = (await metaRepository.FindByConditionAsync(x => x.Value == obj.Value && x.Description == obj.Hash)).FirstOrDefault();
            return data;
        }

        public async Task<(BestCVResponse, EmailMessage<CandidateConfirmEmailBody>?)> ReSendEmail(string email)
        {
            var candidateMeta = (await metaRepository.FindByConditionAsync(x => x.Name == email));

            var candidate = (await repository.FindByConditionAsync(x => x.Email.Equals(email))).FirstOrDefault();

            if (candidateMeta.Count > 0)
            {
                //check email send in day
                int countSent = 0;
                foreach (var row in candidateMeta)
                {
                    if (row.CreatedTime.Date.Equals(DateTime.Now.Date))
                    {
                        countSent++;
                    }
                }

                var randomString = StringExtension.RandomString(8);
                candidateMeta[0].Value = randomString;
                candidateMeta[0].Description = (candidate.Email + "-" + randomString).ToHash256();
                candidateMeta[0].Id = 0;
                candidateMeta[0].CreatedTime = DateTime.Now;

                await metaRepository.CreateAsync(candidateMeta[0]);
                await metaRepository.SaveChangesAsync();

                //check email sent times
                var mes = SendEmailAsync(candidate, candidateMeta[0]);
                if (countSent >= CandidateConstants.PASSSWORD_SENT_EMAIL_LIMIT)
                {
                    return (BestCVResponse.Error(), mes);
                }
                else
                {
                    return (BestCVResponse.Success(), mes);
                }
            }
            else
            {
                return (BestCVResponse.NotFound("", email), null);
            }
        }


        public EmailMessage<CandidateConfirmEmailBody> SendEmailAsync(Candidate can, CandidateMeta canMeta)
        {
            var host = configuration["SectionUrls:CandidateVerifiedNotificationPage"];

            var message = new EmailMessage<CandidateConfirmEmailBody>()
            {
                ToEmails = new List<string> { can.Email },
                CcEmails = new List<String> { },
                BccEmails = new List<string> { },
                Subject = "Xác thực đăng ký tài khoản ứng viên hệ thống BestCV",
                Model = new CandidateConfirmEmailBody()
                {
                    Fullname = can.FullName,
                    Otp = canMeta.Value,
                    ActiveLink = $"{host}/{canMeta.Value}/{canMeta.Description}",
                    Time = DateTime.Now.Year
                }
            };

            return message;
        }



        public async Task<BestCVResponse> UpdateProfileCandidate(UpdateProfileCandidateDTO obj)
        {
            var candidateAccount = await repository.GetByIdAsync(obj.Id);
            if (candidateAccount == null)
            {
                return BestCVResponse.NotFound("Not found", obj);
            }
            var updateAccount = mapper.Map(obj, candidateAccount);
            using (var database = await repository.BeginTransactionAsync())
            {
                try
                {
                    await repository.UpdateAsync(updateAccount);
                    await repository.SaveChangesAsync();
                   
                    if (updateAccount.Id > 0)
                    {
                        #region Update Candidate Experiences
                        // update experiences
                        var listExp = obj.WorkExperiences;
                        var listExpDTOAdded = obj.WorkExperiences.Where(w => w.IsAdded);
                        var listExpDTODeleted = obj.WorkExperiences.Where(w => w.IsDeleted);
                        var listExpDTOUpdated = obj.WorkExperiences.Where(w => w.IsUpdated && !w.IsDeleted);


                        if (listExpDTOAdded != null && listExpDTOAdded.Count() > 0)
                        {
                            var listExpAdd = mapper.Map<IEnumerable<CandidateWorkExperience>>(listExpDTOAdded);
                            foreach (var item in listExpAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateWorkExperienceRepository.CreateListAsync(listExpAdd);
                            await candidateWorkExperienceRepository.SaveChangesAsync();
                        }
                        if (listExpDTOUpdated != null && listExpDTOUpdated.Count() > 0)
                        {
                            var listExpUpdate = mapper.Map<IEnumerable<CandidateWorkExperience>>(listExpDTOUpdated);
                            foreach (var updatedExp in listExpUpdate)
                            {

                                var existingExp = await candidateWorkExperienceRepository.GetByIdAsync(updatedExp.Id);

                                if (existingExp != null)
                                {
                                    existingExp.JobTitle = updatedExp.JobTitle;
                                    existingExp.Company = updatedExp.Company;
                                    existingExp.TimePeriod = updatedExp.TimePeriod;
                                    existingExp.Description = updatedExp.Description;
                                    await candidateWorkExperienceRepository.UpdateAsync(existingExp);
                                }
                            }
                            await candidateWorkExperienceRepository.SaveChangesAsync();
                        }
                        if (listExpDTODeleted != null && listExpDTODeleted.Count() > 0)
                        {
                            var listExpDeleted = mapper.Map<IEnumerable<CandidateWorkExperience>>(listExpDTODeleted);
                            await candidateWorkExperienceRepository.HardDeleteListAsync(listExpDeleted.Select(x => x.Id));
                            await candidateWorkExperienceRepository.SaveChangesAsync();
                        }
                        #endregion
                        #region update Candidate Skill
                        //update Skill                     
                        var listSkill = obj.CandidateSkills;
                        //var listSkillDTOAdded = obj.CandidateSkills.Where(w => w.IsAdded);
                        var listSkillDTODeleted = obj.CandidateSkills.Where(w => w.IsDeleted);

                        if (listSkillDTODeleted != null && listSkillDTODeleted.Count() > 0)
                        {
                            var listSkillDeleted = mapper.Map<IEnumerable<CandidateSkill>>(listSkillDTODeleted);
                            await candidateSkillRepository.HardDeleteListAsync(listSkillDeleted.Select(x => x.Id));
                            await candidateSkillRepository.SaveChangesAsync();
                        }
                        #endregion
                        #region Update CandidateEducations
                        //update Educations
                        var listEdu = obj.Educations;
                        var listEduDTOAdded = obj.Educations.Where(w => w.IsAdded);
                        var listEduDTODeleted = obj.Educations.Where(w => w.IsDeleted);
                        var listEduDTOUpdated = obj.Educations.Where(w => w.IsUpdated && !w.IsDeleted);

                        if (listEduDTOAdded != null && listEduDTOAdded.Count() > 0)
                        {
                            var listEduAdd = mapper.Map<IEnumerable<CandidateEducation>>(listEduDTOAdded);
                            foreach (var item in listEduAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateEducationRepository.CreateListAsync(listEduAdd);
                            await candidateEducationRepository.SaveChangesAsync();
                        }
                        if (listEduDTOUpdated != null && listEduDTOUpdated.Count() > 0)
                        {
                            var listEduUpdate = mapper.Map<IEnumerable<CandidateEducation>>(listEduDTOUpdated);
                            foreach (var updatedEdu in listEduUpdate)
                            {
                             
                                var existingEdu = await candidateEducationRepository.GetByIdAsync(updatedEdu.Id);

                                if (existingEdu != null)
                                {
                                    existingEdu.Title = updatedEdu.Title;
                                    existingEdu.School = updatedEdu.School;
                                    existingEdu.TimePeriod = updatedEdu.TimePeriod;
                                    existingEdu.Description = updatedEdu.Description;
                                    await candidateEducationRepository.UpdateAsync(existingEdu);
                                }
                            }
                            
                            await candidateEducationRepository.SaveChangesAsync();
                        }
                        if (listEduDTODeleted != null && listEduDTODeleted.Count() > 0)
                        {
                            var listEduDeleted = mapper.Map<IEnumerable<CandidateEducation>>(listEduDTODeleted);
                            await candidateEducationRepository.HardDeleteListAsync(listEduDeleted.Select(x => x.Id));
                            await candidateEducationRepository.SaveChangesAsync();
                        }
                        #endregion
                        #region Update CandidateHonorAward
                        //update HonorAward
                        var listHonorAward = obj.HonorAwards;
                        var listHonorAwardDTOAdded = obj.HonorAwards.Where(w => w.IsAdded);
                        var listHonorAwardDTODeleted = obj.HonorAwards.Where(w => w.IsDeleted);
                        var listHonorAwardDTOUpdated = obj.HonorAwards.Where(w => w.IsUpdated && !w.IsDeleted);

                        if (listHonorAwardDTOAdded != null && listHonorAwardDTOAdded.Count() > 0)
                        {
                            var listHonorAwardAdd = mapper.Map<IEnumerable<CandidateHonorAndAward>>(listHonorAwardDTOAdded);
                            foreach (var item in listHonorAwardAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateHonorAwardRepository.CreateListAsync(listHonorAwardAdd);
                            await candidateHonorAwardRepository.SaveChangesAsync();
                        }
                        if (listHonorAwardDTOUpdated != null && listHonorAwardDTOUpdated.Count() > 0)
                        {
                            var listHonorAwardUpdate = mapper.Map<IEnumerable<CandidateHonorAndAward>>(listHonorAwardDTOUpdated);
                            foreach (var updatedHonor in listHonorAwardUpdate)
                            {

                                var existingHonor = await candidateHonorAwardRepository.GetByIdAsync(updatedHonor.Id);

                                if (existingHonor != null)
                                {
                                    existingHonor.Name = updatedHonor.Name;                                  
                                    existingHonor.TimePeriod = updatedHonor.TimePeriod;
                                    existingHonor.Description = updatedHonor.Description;
                                    await candidateHonorAwardRepository.UpdateAsync(existingHonor);
                                }
                            }
                            await candidateHonorAwardRepository.SaveChangesAsync();
                        }
                        if (listHonorAwardDTODeleted != null && listHonorAwardDTODeleted.Count() > 0)
                        {
                            var listHonorAwardDeleted = mapper.Map<IEnumerable<CandidateHonorAndAward>>(listHonorAwardDTODeleted);
                            await candidateHonorAwardRepository.HardDeleteListAsync(listHonorAwardDeleted.Select(x => x.Id));
                            await candidateHonorAwardRepository.SaveChangesAsync();
                        }
                        #endregion
                        #region Update CandidateCertificate
                        //update Certificate
                        var listCertificate = obj.Certificates;
                        var listCertificateDTOAdded = obj.Certificates.Where(w => w.IsAdded);
                        var listCertificateDTODeleted = obj.Certificates.Where(w => w.IsDeleted);
                        var listCertificateDTOUpdated = obj.Certificates.Where(w => w.IsUpdated && !w.IsDeleted);

                        if (listCertificateDTOAdded != null && listCertificateDTOAdded.Count() > 0)
                        {
                            var listCertificateAdd = mapper.Map<IEnumerable<CandidateCertificate>>(listCertificateDTOAdded);
                            foreach (var item in listCertificateAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateCertificateRepository.CreateListAsync(listCertificateAdd);
                            await candidateCertificateRepository.SaveChangesAsync();
                        }
                        if (listCertificateDTOUpdated != null && listCertificateDTOUpdated.Count() > 0)
                        {
                            var listCertificateUpdate = mapper.Map<IEnumerable<CandidateCertificate>>(listCertificateDTOUpdated);
                            foreach (var updatedCertificate in listCertificateUpdate)
                            {

                                var existingCertificate = await candidateCertificateRepository.GetByIdAsync(updatedCertificate.Id);

                                if (existingCertificate != null)
                                {
                                    existingCertificate.Name = updatedCertificate.Name;
                                    existingCertificate.IssueBy = updatedCertificate.IssueBy;
                                    existingCertificate.TimePeriod = updatedCertificate.TimePeriod;
                                    existingCertificate.Description = updatedCertificate.Description;
                                    await candidateCertificateRepository.UpdateAsync(existingCertificate);
                                }
                            }
                            await candidateCertificateRepository.SaveChangesAsync();
                        }
                        if (listCertificateDTODeleted != null && listCertificateDTODeleted.Count() > 0)
                        {
                            var listCertificateDeleted = mapper.Map<IEnumerable<CandidateCertificate>>(listCertificateDTODeleted);
                            await candidateCertificateRepository.HardDeleteListAsync(listCertificateDeleted.Select(x => x.Id));
                            await candidateCertificateRepository.SaveChangesAsync();
                        }
                        #endregion
                        #region Update CandidateActivites
                        //update Activites
                        var listActivites = obj.CandidateActivites;
                        var listActivitesDTOAdded = obj.CandidateActivites.Where(w => w.IsAdded);
                        var listActivitesDTODeleted = obj.CandidateActivites.Where(w => w.IsDeleted);
                        var listActivitesDTOUpdated = obj.CandidateActivites.Where(w => w.IsUpdated && !w.IsDeleted);

                        if (listActivitesDTOAdded != null && listActivitesDTOAdded.Count() > 0)
                        {
                            var listActivitesAdd = mapper.Map<IEnumerable<CandidateActivities>>(listActivitesDTOAdded);
                            foreach (var item in listActivitesAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateActivitesRepository.CreateListAsync(listActivitesAdd);
                            await candidateActivitesRepository.SaveChangesAsync();
                        }
                        if (listActivitesDTOUpdated != null && listActivitesDTOUpdated.Count() > 0)
                        {
                            var listActivitesUpdate = mapper.Map<IEnumerable<CandidateActivities>>(listActivitesDTOUpdated);
                            foreach (var updatedActivites in listActivitesUpdate)
                            {

                                var existingAti = await candidateActivitesRepository.GetByIdAsync(updatedActivites.Id);

                                if (existingAti != null)
                                {
                                    existingAti.Name = updatedActivites.Name;                                   
                                    existingAti.TimePeriod = updatedActivites.TimePeriod;
                                    existingAti.Description = updatedActivites.Description;
                                    await candidateActivitesRepository.UpdateAsync(existingAti);
                                }
                            }
                            await candidateActivitesRepository.SaveChangesAsync();
                        }
                        if (listActivitesDTODeleted != null && listActivitesDTODeleted.Count() > 0)
                        {
                            var listActivitesDeleted = mapper.Map<IEnumerable<CandidateActivities>>(listActivitesDTODeleted);
                            await candidateActivitesRepository.HardDeleteListAsync(listActivitesDeleted.Select(x => x.Id));
                            await candidateActivitesRepository.SaveChangesAsync();
                        }
                        #endregion 
                        #region update CandidateProjects
                        //update Projects
                        var listProjects = obj.CandidateProjects;
                        var listProjectsDTOAdded = obj.CandidateProjects.Where(w => w.IsAdded);
                        var listProjectsDTODeleted = obj.CandidateProjects.Where(w => w.IsDeleted);
                        var listProjectsDTOUpdated = obj.CandidateProjects.Where(w => w.IsUpdated && !w.IsDeleted);

                        if (listProjectsDTOAdded != null && listProjectsDTOAdded.Count() > 0)
                        {
                            var listProjectsAdd = mapper.Map<IEnumerable<CandidateProjects>>(listProjectsDTOAdded);
                            foreach (var item in listProjectsAdd)
                            {
                                item.CandidateId = candidateAccount.Id;
                            }
                            await candidateProjectsRepository.CreateListAsync(listProjectsAdd);
                            await candidateProjectsRepository.SaveChangesAsync();
                        }
                        if (listProjectsDTOUpdated != null && listProjectsDTOUpdated.Count() > 0)
                        {
                            var listProjectsUpdate = mapper.Map<IEnumerable<CandidateProjects>>(listProjectsDTOUpdated);
                            foreach (var updatedProject in listProjectsUpdate)
                            {

                                var existingProject = await candidateProjectsRepository.GetByIdAsync(updatedProject.Id);

                                if (existingProject != null)
                                {
                                    existingProject.ProjectName = updatedProject.ProjectName;
                                    existingProject.Customer = updatedProject.Customer;
                                    existingProject.TeamSize = updatedProject.TeamSize;
                                    existingProject.Position = updatedProject.Position;
                                    existingProject.TimePeriod = updatedProject.TimePeriod;
                                    existingProject.Info = updatedProject.Info;
                                    await candidateProjectsRepository.UpdateAsync(existingProject);
                                }
                            }
                            await candidateProjectsRepository.SaveChangesAsync();
                        }
                        if (listProjectsDTODeleted != null && listProjectsDTODeleted.Count() > 0)
                        {
                            var listProjectsDeleted = mapper.Map<IEnumerable<CandidateProjects>>(listProjectsDTODeleted);
                            await candidateProjectsRepository.HardDeleteListAsync(listProjectsDeleted.Select(x => x.Id));
                            await candidateProjectsRepository.SaveChangesAsync();
                        }                     
                        #endregion
                        await repository.EndTransactionAsync();
                        return BestCVResponse.Success("Cập nhật thông tin thành công");
                    }

                    else
                    {
                        await repository.RollbackTransactionAsync();
                        return (BestCVResponse.Error("Cập nhật ứng viên không thành công"));
                    }
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, $"Có lỗi khi cập nhật thông tin ứng viên{obj.Id}");
                    await repository.RollbackTransactionAsync();
                    return (BestCVResponse.Error());
                }
            }
        }
        

        public async Task<BestCVResponse> UpdateCandidateJobSuggestionSetting(UpdateCandidateJobSuggetionSettingDTO obj)
        {
            var candidateJobSuggestion = await repository.GetByIdAsync(obj.Id);
            if (candidateJobSuggestion == null)
            {
                return BestCVResponse.NotFound("Not found", obj);
            }
            candidateJobSuggestion.SuggestionSalaryRangeId = obj.salaryRangeId;
            candidateJobSuggestion.SuggestionExperienceRangeId = obj.experienceRangeId;
            candidateJobSuggestion.Gender = obj.Gender;
            var database = await repository.BeginTransactionAsync();
            using (database)
            {
                try
                {
                    await repository.UpdateAsync(candidateJobSuggestion);
                    await repository.SaveChangesAsync();
                    var jobCategory = await candidateSuggestionJobCategoryRepository.GetByCandidateJobCategoryIdAsync(obj.Id);
                    if (jobCategory == null)
                    {
                        var newSuggestionCategory = new CandidateSuggestionJobCategory()
                        {
                            Id = 0,
                            CreatedTime = DateTime.Now,
                            Active = true,
                            CandidateId = obj.Id,
                            JobCategoryId = obj.JobCategoryId
                        };
                        await candidateSuggestionJobCategoryRepository.CreateAsync(newSuggestionCategory);
                        await candidateSuggestionJobCategoryRepository.SaveChangesAsync();
                    }
                    else
                    {
                        jobCategory.CandidateId = obj.Id;
                        jobCategory.JobCategoryId = obj.JobCategoryId;
                        await candidateSuggestionJobCategoryRepository.UpdateAsync(jobCategory);
                        await candidateSuggestionJobCategoryRepository.SaveChangesAsync();
                    }

                    var jobPosition = await candidateSuggestionJobPositionRepository.GetByCandidateJobPositionIdAsync(obj.Id);
                    if (jobPosition == null)
                    {
                        var newSuggestionPosition = new CandidateSuggestionJobPosition()
                        {
                            Id = 0,
                            CreatedTime = DateTime.Now,
                            Active = true,
                            CandidateId = obj.Id,
                            JobPositionId = obj.JobPositionId,
                        };
                        await candidateSuggestionJobPositionRepository.CreateAsync(newSuggestionPosition);
                        await candidateSuggestionJobPositionRepository.SaveChangesAsync();
                    }
                    else
                    {
                        jobPosition.CandidateId = obj.Id;
                        jobPosition.JobPositionId = obj.JobPositionId;
                        await candidateSuggestionJobPositionRepository.UpdateAsync(jobPosition);
                        await candidateSuggestionJobPositionRepository.SaveChangesAsync();
                    }

                    var jobSkill = await candidateSuggestionJobSkillRepository.GetByCandidateJobSkillIdAsync(obj.Id);
                    if (jobSkill == null)
                    {
                        var newSuggestionJobSkill = new CandidateSuggestionJobSkill()
                        {
                            Id = 0,
                            CreatedTime = DateTime.Now,
                            Active = true,
                            CandidateId = obj.Id,
                            JobSkillId = obj.JobSkillId,
                        };
                        await candidateSuggestionJobSkillRepository.CreateAsync(newSuggestionJobSkill);
                        await candidateSuggestionJobSkillRepository.SaveChangesAsync();
                    }
                    else
                    {
                        jobSkill.CandidateId = obj.Id;
                        jobSkill.JobSkillId = obj.JobSkillId;
                        await candidateSuggestionJobSkillRepository.UpdateAsync(jobSkill);
                        await candidateSuggestionJobSkillRepository.SaveChangesAsync();
                    }

                    var workPlace = await candidateSuggestionWorkPlaceRepository.GetByCandidateWorkPlaceIdAsync(obj.Id);
                    if (workPlace == null)
                    {
                        var newSuggestionWorkPlace = new CandidateSuggestionWorkPlace()
                        {
                            Id = 0,
                            CreatedTime = DateTime.Now,
                            Active = true,
                            CandidateId = obj.Id,
                            WorkPlaceId = obj.workPlaceId
                        };
                        await candidateSuggestionWorkPlaceRepository.CreateAsync(newSuggestionWorkPlace);
                        await candidateSuggestionWorkPlaceRepository.SaveChangesAsync();
                    }
                    else
                    {
                        workPlace.CandidateId = obj.Id;
                        workPlace.WorkPlaceId = obj.workPlaceId;
                        await candidateSuggestionWorkPlaceRepository.UpdateAsync(workPlace);
                        await candidateSuggestionWorkPlaceRepository.SaveChangesAsync();
                    }

                    await repository.EndTransactionAsync();
                    return BestCVResponse.Success("Cập nhật gợi ý việc làm thành công");
                }
                catch (Exception e)
                {
                    logger.LogError(e, $"Failed to update: {obj}");
                    await repository.RollbackTransactionAsync();
                    return BestCVResponse.Error("Cập nhật gợi ý việc làm không thành công.");
                }
            }
        }


        public async Task<BestCVResponse> SignIn(SigninCandidateDTO obj)
        {
            List<string> errors = new();
            Candidate? candidate = null;
            if (obj.EmailorPhone.Contains('@'))
            {
                candidate = await repository.GetByEmailAsync(obj.EmailorPhone);
            }
            else
            {
                candidate = await repository.GetByPhoneAsync(obj.EmailorPhone);
            }

            if (candidate != null && candidate.Password.Equals(obj.Password.ToHash256()))
            {
                if (candidate.IsActivated)
                {
                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Username = candidate.Username,
                        Phone = candidate.Phone
                    };
                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}.", accountToken.Email, DateTime.UtcNow);

                    LoginCandidateModel loginModel = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };
                    return BestCVResponse.Success(loginModel);
                }
                errors.Add("Tài khoản của bạn chưa được kích hoạt.");
                return BestCVResponse.BadRequest(errors);
            }
            errors.Add("Sai thông tin tài khoản hoặc mật khẩu.");
            return BestCVResponse.BadRequest(errors);
        }


        public async Task<BestCVResponse> SignInWithFacebook(SignInWithSocialNetworkDTO obj)
        {
            var candidate = new Candidate();
            if (obj.Email.Contains("@"))
            {
                if (!EmailValidate(obj.Email))
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_EMAIL_INVALID);
                }

                candidate = await repository.GetByEmailAsync(obj.Email);
            }
            else
            {
                candidate = await repository.CheckCandidateByFacebookId(obj.Id);
            }

            if (candidate == null)
            {
                var newCandidate = new Candidate()
                {
                    Active = true,
                    Email = obj.Email,
                    CreatedTime = DateTime.Now,
                    Password = "",
                    FacebookId = obj.Id,
                    Phone = "",
                    Id = 0,
                    Photo = CandidateConstants.DEFAULT_PHOTO,
                    CoverPhoto = CandidateConstants.DEFAULT_CONVER_PHOTO,
                    CandidateLevelId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_LEVEL,
                    CandidateStatusId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_STATUS,
                    IsActivated = true,
                    FullName = obj.FullName,
                    Username = obj.FullName,
                    Search = obj.Phone + "-" + obj.FullName + "-" + "-" + obj.Email,
                };

                await repository.CreateAsync(newCandidate);
                await repository.SaveChangesAsync();

                if (newCandidate.Id > 0)
                {
                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Phone = candidate.Phone,
                        Username = candidate.FullName
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);

                    LoginCandidateModel loginCandidate = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };
                    return BestCVResponse.Success(loginCandidate);
                    //return BestCVResponse.Success(newCandidate);
                }
                else
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_WHEN_SIGN_IN_SNSID);
                }
            }
            else
            {
                if (!candidate.Active)
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_NOT_ACTIVATED);
                }
                else
                {
                    candidate.FacebookId = obj.Id;
                    await repository.UpdateFacebookId(candidate);
                    await repository.SaveChangesAsync();

                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Phone = candidate.Phone,
                        Username = candidate.FullName
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);

                    LoginCandidateModel loginCandidate = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };

                    return BestCVResponse.Success(loginCandidate);
                }
            }
        }

        public bool EmailValidate(string email)
        {
            bool isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<BestCVResponse> SignInWithGoole(SignInWithSocialNetworkDTO obj)
        {
            if (!EmailValidate(obj.Email))
            {
                return BestCVResponse.Error(CandidateConstants.ERROR_EMAIL_INVALID);
            }
            var candidate = await repository.GetByEmailAsync(obj.Email);
            if (candidate == null)
            {
                var newCandidate = new Candidate()
                {
                    Active = true,
                    Email = obj.Email,
                    CreatedTime = DateTime.Now,
                    Password = "",
                    GoogleId = obj.Id,
                    Phone = "",
                    Id = 0,
                    Photo = CandidateConstants.DEFAULT_PHOTO,
                    CoverPhoto = CandidateConstants.DEFAULT_CONVER_PHOTO,
                    CandidateLevelId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_LEVEL,
                    CandidateStatusId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_STATUS,
                    IsActivated = true,
                    FullName = obj.FullName,
                    Username = obj.FullName,
                    Search = obj.Phone + "-" + obj.FullName + "-" + "-" + obj.Email,
            };
                await repository.CreateAsync(newCandidate);
                await repository.SaveChangesAsync();

                AccountToken accountToken = new()
                {
                    Id = newCandidate.Id,
                    Email = newCandidate.Email,
                    Phone = newCandidate.Phone,
                    Username = newCandidate.FullName
                };
                string token = tokenService.GenerateToken(accountToken);
                logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);
                LoginCandidateModel loginCandidate = new()
                {
                    Fullname = newCandidate.FullName,
                    Photo = newCandidate.Photo,
                    Token = token,
                };
               
                return BestCVResponse.Success(loginCandidate);

            }
            else
            {
                if (!candidate.Active)
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_NOT_ACTIVATED);
                }
                else
                {

                    candidate.GoogleId = obj.Id;
                    await repository.UpdateGoogleId(candidate);
                    await repository.SaveChangesAsync();

                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Phone = candidate.Phone,
                        Username = candidate.FullName
                    };
                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);
                    LoginCandidateModel loginCandidate = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };

                    return BestCVResponse.Success(loginCandidate);

                }
            }
        }

        public async Task<BestCVResponse> SignInWithLinkedIn(SignInWithSocialNetworkDTO obj)
        {
            var candidate = new Candidate();
            if (obj.Email.Contains("@"))
            {
                if (!EmailValidate(obj.Email))
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_EMAIL_INVALID);
                }

                candidate = await repository.GetByEmailAsync(obj.Email);
            }
            else
            {
                candidate = await repository.CheckCandidateByLinkedinId(obj.Id);
            }

            if (candidate == null)
            {
                var newCandidate = new Candidate()
                {
                    Active = true,
                    Email = obj.Email,
                    CreatedTime = DateTime.Now,
                    Password = "",
                    LinkedinId = obj.Id,
                    Phone = "",
                    Id = 0,
                    CoverPhoto = CandidateConstants.DEFAULT_CONVER_PHOTO,
                    CandidateLevelId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_LEVEL,
                    CandidateStatusId = CandidateConstants.DEFAULT_VALUE_CANDIDATE_STATUS,
                    IsActivated = true,
                    FullName = obj.FullName,
                    Username = obj.FullName,
                    Search = obj.Phone + "-" + obj.FullName + "-" + "-" + obj.Email,
                    Photo = obj.Photo != null ? obj.Photo : CandidateConstants.DEFAULT_PHOTO,
                };

                await repository.CreateAsync(newCandidate);
                await repository.SaveChangesAsync();

                if (newCandidate.Id > 0)
                {
                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Phone = candidate.Phone,
                        Username = candidate.FullName
                    };

                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);

                    LoginCandidateModel loginCandidate = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };
                    return BestCVResponse.Success(loginCandidate);
                    //return BestCVResponse.Success(newCandidate);
                }
                else
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_WHEN_SIGN_IN_SNSID);
                }
            }
            else
            {
                if (!candidate.Active)
                {
                    return BestCVResponse.Error(CandidateConstants.ERROR_NOT_ACTIVATED);
                }
                else
                {
                    candidate.LinkedinId = obj.Id;
                    await repository.UpdateLinkedinId(candidate);
                    await repository.SaveChangesAsync();

                    AccountToken accountToken = new()
                    {
                        Id = candidate.Id,
                        Email = candidate.Email,
                        Phone = candidate.Phone,
                        Username = candidate.FullName
                    };
                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}", accountToken.Email, DateTime.Now);
                    LoginCandidateModel loginCandidate = new()
                    {
                        Fullname = candidate.FullName,
                        Photo = candidate.Photo,
                        Token = token,
                    };

                    return BestCVResponse.Success(candidate);
                }
            }
        }

        public async Task<object> ListCandidateAggregates(CandidateDTParameters parameters)
        {
            return await repository.ListCandidateAggregates(parameters);
        }

        public async Task<BestCVResponse> QuickActivatedAsync(long id)
        {
            var isUpdated = await repository.QuickActivatedAsync(id);
            if (isUpdated)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.BadRequest("Kích hoạt ứng viên không thành công");
        }

        public async Task<BestCVResponse> ChangePasswordAdminAsync(ChangePasswordDTO obj)
        {
            var newCandidate = await repository.GetByIdAsync(obj.Id);
            if (newCandidate == null)
            {
                return BestCVResponse.NotFound("Không tìm thấy ứng viên", newCandidate);
            }
            if (newCandidate.Password == obj.NewPassword.ToHash256())
            {
                return BestCVResponse.Error("Mật khẩu mới không được trùng với mật khẩu đã đặt");

            }
            newCandidate.Password = obj.NewPassword.ToHash256();

            var isUpdated = await repository.ChangePasswordAsync(newCandidate);
            if (isUpdated)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.Error("Thay đổi mật khẩu không thành công");
        }

        public async Task<BestCVResponse> ExportExcel(List<CandidateAggregates> data)
        {
            if (data.Count > 0)
            {
                var fileName = "DS_Ung_Vien_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx";
                XSSFWorkbook wb = new XSSFWorkbook();
                // Tạo ra 1 sheet
                ISheet sheet = wb.CreateSheet();
                sheet.DisplayGridlines = false;
                var rowTitle = sheet.CreateRow(0);
                CellRangeAddress regionTitle = new CellRangeAddress(0, 1, 0, 6);
                sheet.AddMergedRegion(regionTitle);
                ICell cellTitle = rowTitle.CreateCell(0);
                cellTitle.SetCellValue("DANH SÁCH ỨNG VIÊN");
                // Ghi tiêu đề cột ở row 1
                var row1 = sheet.CreateRow(2);
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("STT");
                ICell cell1 = row1.CreateCell(1);
                cell1.SetCellValue("HỌ TÊN");
                ICell cell2 = row1.CreateCell(2);
                cell2.SetCellValue("TÊN ĐĂNG NHẬP");
                ICell cell3 = row1.CreateCell(3);
                cell3.SetCellValue("CẤP");
                ICell cell4 = row1.CreateCell(4);
                cell4.SetCellValue("TRẠNG THÁI");
                ICell cell5 = row1.CreateCell(5);
                cell5.SetCellValue("NGÀY TẠO");
                ICell cell6 = row1.CreateCell(6);
                cell6.SetCellValue("KÍCH HOẠT");
                //style row title
                ICellStyle styleTitle = wb.CreateCellStyle();
                styleTitle.Alignment = HorizontalAlignment.Center;
                styleTitle.VerticalAlignment = VerticalAlignment.Center;
                //style border 
                ICellStyle styleBorder = wb.CreateCellStyle();
                styleBorder.BorderBottom = BorderStyle.Thin;
                styleBorder.BottomBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderTop = BorderStyle.Thin;
                styleBorder.TopBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderLeft = BorderStyle.Thin;
                styleBorder.LeftBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderRight = BorderStyle.Thin;
                styleBorder.RightBorderColor = HSSFColor.Black.Index;
                //style stt
                ICellStyle styleStt = wb.CreateCellStyle();
                styleStt.Alignment = HorizontalAlignment.Center;
                styleStt.BorderBottom = BorderStyle.Thin;
                styleStt.BottomBorderColor = HSSFColor.Black.Index;
                styleStt.BorderTop = BorderStyle.Thin;
                styleStt.TopBorderColor = HSSFColor.Black.Index;
                styleStt.BorderLeft = BorderStyle.Thin;
                styleStt.LeftBorderColor = HSSFColor.Black.Index;
                styleStt.BorderRight = BorderStyle.Thin;
                styleStt.RightBorderColor = HSSFColor.Black.Index;
                //style row 1
                ICellStyle styleRow1 = wb.CreateCellStyle();
                styleRow1.FillForegroundColor = HSSFColor.PaleBlue.Index;
                styleRow1.FillPattern = FillPattern.SolidForeground;
                styleRow1.BorderBottom = BorderStyle.Thin;
                styleRow1.BottomBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderTop = BorderStyle.Thin;
                styleRow1.TopBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderLeft = BorderStyle.Thin;
                styleRow1.LeftBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderRight = BorderStyle.Thin;
                styleRow1.RightBorderColor = HSSFColor.Black.Index;
                //font
                var boldFont = wb.CreateFont();
                boldFont.IsBold = true;
                styleRow1.SetFont(boldFont);
                var boldFontTitle = wb.CreateFont();
                boldFontTitle.IsBold = true;
                boldFontTitle.FontHeightInPoints = 18;
                styleTitle.SetFont(boldFontTitle);
                //set style row 1row1
                cell.CellStyle = styleRow1;
                cell1.CellStyle = styleRow1;
                cell2.CellStyle = styleRow1;
                cell3.CellStyle = styleRow1;
                cell4.CellStyle = styleRow1;
                cell5.CellStyle = styleRow1;
                cell6.CellStyle = styleRow1;
                cellTitle.CellStyle = styleTitle;
                //style date time 
                IDataFormat dataDateFormatCustom = wb.CreateDataFormat();
                ICellStyle styleDateTime = wb.CreateCellStyle();
                styleDateTime.DataFormat = dataDateFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");
                styleDateTime.BorderBottom = BorderStyle.Thin;
                styleDateTime.BottomBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderTop = BorderStyle.Thin;
                styleDateTime.TopBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderLeft = BorderStyle.Thin;
                styleDateTime.LeftBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderRight = BorderStyle.Thin;
                styleDateTime.RightBorderColor = HSSFColor.Black.Index;
                //ghi dữ liệu
                int rowIndex = 3;
                int sttCount = 1;
                foreach (var item in data)
                {
                    var newRow = sheet.CreateRow(rowIndex);
                    var countNumberCell = newRow.CreateCell(0);
                    countNumberCell.SetCellValue(sttCount);
                    countNumberCell.CellStyle = styleStt;
                    var fullNameCell = newRow.CreateCell(1);
                    fullNameCell.SetCellValue(item.FullName);
                    fullNameCell.CellStyle = styleBorder;
                    var userNameCell = newRow.CreateCell(2);
                    userNameCell.SetCellValue(item.Username);
                    userNameCell.CellStyle = styleBorder;
                    var levelCell = newRow.CreateCell(3);
                    levelCell.SetCellValue(item.CandidateLevelName);
                    levelCell.CellStyle = styleBorder;
                    var statusCell = newRow.CreateCell(4);
                    statusCell.SetCellValue(item.CandidateStatusName);
                    statusCell.CellStyle = styleBorder;
                    var createdTimeCell = newRow.CreateCell(5);
                    createdTimeCell.SetCellValue(item.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    createdTimeCell.CellStyle = styleDateTime;
                    var activatedCell = newRow.CreateCell(6);
                    activatedCell.SetCellValue(item.IsActivated ? "Đã kích hoạt" : "Chưa kích hoạt");
                    activatedCell.CellStyle = styleBorder;
                    sttCount++;
                    rowIndex++;
                }
                var rowSign = sheet.CreateRow(rowIndex + 1);
                CellRangeAddress regionSign = new CellRangeAddress(rowIndex + 1, rowIndex + 1, 5, 6);
                sheet.AddMergedRegion(regionSign);
                ICell cellSign = rowSign.CreateCell(6);
                cellSign.SetCellValue("Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year);
                ICellStyle styleSign = wb.CreateCellStyle();
                styleSign.VerticalAlignment = VerticalAlignment.Center;
                styleSign.Alignment = HorizontalAlignment.Center;
                cellSign.CellStyle = styleSign;
                //auto size column
                sheet.AutoSizeColumn(0);
                for (int i = 1; i <= 6; i++)
                {
                    sheet.AutoSizeColumn(i);
                    sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + sheet.GetColumnWidth(i) / 2);
                }
                string handle = "ListCandidateExport" + DateTime.Now.Ticks.ToString();
                using (var memory = new MemoryStream())
                {
                    wb.Write(memory);
                    var byte_array = memory.ToArray();
                    //TempData[handle] = byte_array;
                    _files[handle] = byte_array;
                }
                var obj = new
                {
                    FileGuid = handle,
                    FileName = fileName,
                };
                return BestCVResponse.Success(obj);
            }
            else
            {
                return BestCVResponse.BadRequest("Failed to export Excel");
            }
        }

        public byte[] DownloadExcel(string fileGuid, string fileName)
        {
            if (_files.ContainsKey(fileGuid))
            {
                var data = _files[fileGuid];
                _files.Remove(fileGuid);
                return data;
            }
            else
            {
                return null;
            }
        }


        public async Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email)
        {
            var candidate = await repository.FindByEmail(email);
            var database = await metaRepository.BeginTransactionAsync();
            using (database)
            {
                try
                {
                    if (candidate.Id > 0)
                    {
                        var randomString = StringExtension.RandomString(8);
                        var candidateMeta = new CandidateMeta()
                        {
                            Id = 0,
                            Active = true,
                            Name = candidate.Username,
                            CreatedTime = DateTime.Now,
                            CandidateId= candidate.Id,
                            Key = CandidateConstants.FORGOT_PASSWORD_EMAIL_KEY,
                            Value = randomString,
                            Description = (candidate.Email + "-" + randomString).ToHash256()
                        };

                        // thêm vào bảng employerMeta 
                        await metaRepository.CreateAsync(candidateMeta);
                        // lưu thay đổi trong bảng employerMeta
                        await metaRepository.SaveChangesAsync();
                        if (candidateMeta.Id > 0)
                        {
                            // setup email 
                            var message = SendEmailForgotPasswordAsync(candidate, candidateMeta);
                            // hoàn thành transaction
                            await metaRepository.EndTransactionAsync();
                            return (BestCVResponse.Success(), message);
                        }
                        else
                        {
                            await metaRepository.RollbackTransactionAsync();
                            return (BestCVResponse.Error("Email chưa kích hoạt. "), null);
                        }
                    }
                    else
                    {
                        await metaRepository.RollbackTransactionAsync();
                        return (BestCVResponse.Error("Email chưa kích hoạt."), null);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Có lỗi khi gửi email");
                    return (BestCVResponse.Error(), null);
                }
            }

        }

        public EmailMessage<ForgotPasswordEmailBody> SendEmailForgotPasswordAsync(Candidate candidate, CandidateMeta candidateMeta)
        {
            var host = configuration["SectionUrls:CandidateVerifiedForgotPasswordNotificationPage"];

            var message = new EmailMessage<ForgotPasswordEmailBody>()
            {
                ToEmails = new List<string> { candidate.Email },
                CcEmails = new List<string> { },
                BccEmails = new List<string> { },
                Subject = "Thay đổi mật khẩu tài khoản ứng viên hệ thống Jobi",
                Model = new ForgotPasswordEmailBody()
                {
                    Fullname = candidate.FullName,
                    ActiveLink = $"{host}/{candidateMeta.Value}/{candidateMeta.Description}",
                    Time = DateTime.Now.Year,
                    validExpried = CandidateConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT,

                }
            };
            return message;
        }

        public async Task<int> CountResetPassword(long candidateMeta)
        {
            return await metaRepository.CountResetPassword(candidateMeta);
        }

        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await repository.CheckEmailIsActive(email);
        }

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            return await repository.GetByEmailAsync(email);
        }

        public async Task<bool> CheckKeyValid(string code, string hash)
        {
            var result = false;
            var dataMeta = await metaRepository.CheckVerifyCode(code, hash);
            if (dataMeta != null)
            {
                var time = DateTime.Now - dataMeta.CreatedTime;
                if (time < TimeSpan.FromHours(CandidateConstants.FORGOT_PASSWORD_TOKEN_EXPRIED))
                {
                    result = true;
                }
            }
            return result;
        }

        public async Task<BestCVResponse> ResetNewPassword(string code, string hash, string password)
        {
            var dataMeta = await metaRepository.CheckVerifyCode(code, hash);
            var candidate = await repository.GetByIdAsync(dataMeta.CandidateId);
            candidate.Password = password.ToHash256();
            await repository.UpdateAsync(candidate);
            await repository.SaveChangesAsync();
            dataMeta.Active = false;
            await metaRepository.UpdateAsync(dataMeta);
            await metaRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<bool> IsActive(long id)
        {
            var candidate = await repository.GetByIdAsync(id);
            if(candidate!=null && candidate.Active)
            {
                return true;
            }
            return false;
        }

        public async Task<object> FindCandidateAgrregates(FindCandidateParameters parameters)
        {
            return await repository.FindCandidateAgrregates(parameters);

        }
    }  
}
