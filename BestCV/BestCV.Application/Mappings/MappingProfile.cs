using AutoMapper;
using BestCV.Application.Mappings;
using BestCV.Application.Models.AdminAccountRoles;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Models.WorkPlace;
using BestCV.Application.Models.Occupation;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Models.Employer;
using BestCV.Application.Models.InterviewStatsus;
using BestCV.Application.Models.JobCategory;
using BestCV.Application.Models.MultimediaType;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PaymentMethod;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Models.Coupon;
using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.ExperienceRange;
using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.JobSkill;
using BestCV.Application.Models.Menu;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Models.JobStatuses;
using BestCV.Application.Models.Permissions;
using BestCV.Application.Models.RolePermissions;
using BestCV.Application.Models.Roles;
using BestCV.Domain.Entities;
using BestCV.Application.Models.NotificationType;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.PostLayout;
using BestCV.Application.Models.PostStatus;
using BestCV.Application.Models.JobType;
using BestCV.Application.Models.FolderUploads;
using BestCV.Application.Models.Position;
using BestCV.Application.Models.SkillLevel;
using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostTag;
using BestCV.Application.Models.Tag;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.SalaryRange;
using BestCV.Application.Models.CandidateWorkExperience;
using BestCV.Application.Models.CandidateSkill;
using BestCV.Application.Models.CandidateEducation;
using BestCV.Application.Models.CandidateActivites;
using BestCV.Application.Models.CandidateCertificate;
using BestCV.Application.Models.CandidateHonorAward;
using BestCV.Application.Models.CandidateProjects;
using BestCV.Application.Models.Slides;
using BestCV.Application.Models.SystemConfigs;
using BestCV.Application.Models.NotificationStatuses;
using BestCV.Application.Models.InterviewTypes;
using BestCV.Application.Models.OrderTypes;
using BestCV.Application.Models.VoucherTypes;
using BestCV.Application.Models.EmployerActivityLogTypes;
using BestCV.Application.Models.CandidateApplyJobStatuses;
using BestCV.Application.Models.RecruitmentCampaignStatuses;
using BestCV.Application.Models.Company;
using BestCV.Application.Models.License;
using BestCV.Application.Models.LicenseType;
using BestCV.Application.Models.EmployerServicePackage;
using BestCV.Application.Models.CompanySize;
using BestCV.Application.Models.CandidateSuggestionJobCategory;
using BestCV.Application.Models.CandidateSuggestionJobPosition;
using BestCV.Application.Models.CandidateSuggestionJobSkill;
using BestCV.Application.Models.CandidateSuggestionWorkPlace;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.EmployerServicePackageEmployerBenefit;
using BestCV.Application.Models.FieldOfActivities;
using BestCV.Application.Models.CompanyFieldOfActivities;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Application.Models.TopCompany;
using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.CandidateApplyJobSources;
using BestCV.Application.Models.Job;
using BestCV.Application.Models.JobSecondaryJobPositions;
using BestCV.Application.Models.JobRequireCity;
using BestCV.Application.Models.TopFeatureJob;
using BestCV.Application.Models.RecruitmentCampaigns;
using BestCV.Application.Models.CandidateFollowCompany;
using BestCV.Application.Models.Skill;
using BestCV.Application.Models.CandidateCVPDF;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Models.InterviewSchdule;
using BestCV.Application.Models.EmployerNotification;
using BestCV.Domain.Aggregates.Job;
using BestCV.Application.Models.TagType;
using BestCV.Application.Models.MenuType;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Models.TopJobUrgent;
using BestCV.Application.Models.JobSuitable;
using BestCV.Application.Models.MustBeInterestedCompany;
using BestCV.Application.Models.CandidateCVs;
using BestCV.Application.Models.ServicePackageGroup;
using BestCV.Application.Models.ServicePackageType;
using BestCV.Application.Models.ServicePackageBenefit;
using BestCV.Application.Models.TopJobManagement;
using BestCV.Application.Models.EmployerOrder;
using BestCV.Application.Models.EmployerCarts;
using BestCV.Application.Models.JobReference;
using BestCV.Application.Models.EmployerWallet;
using BestCV.Application.Models.EmployerWalletHistory;

namespace BestCV.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region AdminAccount
            CreateMap<InsertAdminAccountDTO, AdminAccount>();
            CreateMap<UpdateAdminAccountDTO, AdminAccount>().IgnoreAllNonExisting();
            CreateMap<AdminAccount, AccountStatusDTO>().ReverseMap();
            #endregion
            #region Role
            CreateMap<InsertRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>().IgnoreAllNonExisting();
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion
            #region Permission
            CreateMap<InsertPermissionDTO, Permission>();
            CreateMap<UpdatePermissionDTO, Permission>().IgnoreAllNonExisting();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            #endregion
            #region RolePermisson
            CreateMap<InsertRolePermissionDTO, RolePermission>();
            CreateMap<UpdateRolePermissionDTO, RolePermission>().IgnoreAllNonExisting();
            CreateMap<RolePermission, RolePermissionDTO>().ReverseMap();
            #endregion
            #region AdminAccountRole
            CreateMap<InsertAdminAccountRoleDTO, AdminAccountRole>();
            CreateMap<UpdateAdminAccountRoleDTO, AdminAccountRole>().IgnoreAllNonExisting();
            CreateMap<AdminAccountRole, AdminAccountRoleDTO>().ReverseMap();
            #endregion
            #region FolderUpload
            CreateMap<InsertFolderUploadDTO, FolderUploadDTO>();
            CreateMap<FolderUpload, FolderUploadDTO>().ReverseMap();
            #endregion
            #region SystemConfig
            CreateMap<InsertSystemConfigDTO, SystemConfig>();
            CreateMap<UpdateSystemConfigDTO, SystemConfig>().IgnoreAllNonExisting();
            CreateMap<SystemConfig, SystemConfigDTO>().ReverseMap();
            #endregion
            #region Slide
            CreateMap<InsertSlideDTO, Slide>();
            CreateMap<UpdateSlideDTO, Slide>().IgnoreAllNonExisting();
            CreateMap<SlideDTO, Slide>();
            #endregion
            #region Notification Status
            CreateMap<InsertNotificationStatusDTO, NotificationStatus>();
            CreateMap<UpdateNotificationStatusDTO, NotificationStatus>().IgnoreAllNonExisting();
            CreateMap<NotificationStatus, NotificationStatusDTO>().ReverseMap();
            #endregion
            #region InterviewType
            CreateMap<InsertInterviewTypeDTO, InterviewType>();
            CreateMap<UpdateInterviewTypeDTO, InterviewType>().IgnoreAllNonExisting();
            CreateMap<InterviewType, InterviewTypeDTO>().ReverseMap();
            #endregion
            #region InterviewSchedule
            CreateMap<InsertInterviewScheduleDTO, InterviewSchedule>();
            CreateMap<UpdateInterviewScheduleDTO, InterviewSchedule>().IgnoreAllNonExisting();
            CreateMap<InterviewSchedule, InterviewScheduleDTO>().ReverseMap();
            #endregion
            #region OrderType
            CreateMap<InsertOrderTypeDTO, OrderType>();
            CreateMap<UpdateOrderTypeDTO, OrderType>().IgnoreAllNonExisting();
            CreateMap<OrderType, OrderTypeDTO>().ReverseMap();
            #endregion
            #region VoucherType
            CreateMap<InsertVoucherTypeDTO, VoucherType>();
            CreateMap<UpdateVoucherTypeDTO, VoucherType>().IgnoreAllNonExisting();
            CreateMap<VoucherType, VoucherTypeDTO>().ReverseMap();
            #endregion
            #region EmployerActivityLogType
            CreateMap<InsertEmployerActivityLogTypeDTO, EmployerActivityLogType>();
            CreateMap<UpdateEmployerActivityLogTypeDTO, EmployerActivityLogType>().IgnoreAllNonExisting();
            CreateMap<EmployerActivityLogType, EmployerActivityLogTypeDTO>().ReverseMap();
            #endregion
            #region RecruitmentCampaingnStatus
            CreateMap<InsertRecruitmentStatusDTO, RecruitmentStatus>();
            CreateMap<UpdateRecruitmentStatusDTO, RecruitmentStatus>().IgnoreAllNonExisting();
            CreateMap<RecruitmentCampaignStatus, RecruitmentCampaignStatusDTO>().ReverseMap();
            #endregion
            #region RecruitmentCampaingnStatus
            CreateMap<InsertRecruitmentCampaignStatusDTO, RecruitmentCampaignStatus>();
            CreateMap<UpdateRecruitmentCampaignDTO, RecruitmentCampaignStatus>().IgnoreAllNonExisting();
            CreateMap<RecruitmentCampaignStatus, RecruitmentCampaignStatusDTO>().ReverseMap();
            #endregion
            #region CandidateApplyJobStatus
            CreateMap<InsertCandidateApplyJobStatusDTO, CandidateApplyJobStatus>();
            CreateMap<UpdateCandidateApplyJobStatusDTO, CandidateApplyJobStatus>().IgnoreAllNonExisting();
            CreateMap<CandidateApplyJobStatus, CandidateApplyJobStatusDTO>().ReverseMap();
			#endregion
			#region FieldOfActivity
			CreateMap<InsertFieldOfActivityDTO, FieldOfActivity>();
			CreateMap<UpdateFieldOfActivityDTO, FieldOfActivity>().ReverseMap();
			CreateMap<FieldOfActivityDTO, FieldOfActivity>().IgnoreAllNonExisting();
			#endregion
			#region CompanyFieldOfActivity
			CreateMap<InsertCompanyFieldOfActivityDTO, CompanyFieldOfActivity>();
			CreateMap<UpdateCompanyFieldOfActivityDTO, CompanyFieldOfActivity>().IgnoreAllNonExisting();
			CreateMap<CompanyFieldOfActivity, CompanyFieldOfActivityDTO>().ReverseMap();
            #endregion
            #region CandidateApplyJob
            CreateMap<CandidateApplyJobDTO, CandidateApplyJob>().ReverseMap();
            CreateMap<InsertCandidateApplyJobDTO, CandidateApplyJob>();
            CreateMap<AddNoteCandidateApplyJobDTO, CandidateApplyJob>().IgnoreAllNonExisting();
            CreateMap<ChangeStatusCandidateApplyJobDTO, CandidateApplyJob>().IgnoreAllNonExisting();
            CreateMap<CandidateApplyJobDTO, CandidateApplyJob>().ReverseMap();
            CreateMap<CandidateApplyJob, CandidateApplyJobDTO>()
            .ForMember(dto => dto.CandidateName, opt => opt.MapFrom(src => src.Candidate.FullName))
            .ForMember(dto => dto.CandidateId, opt => opt.MapFrom(src => src.Candidate.Id));

            #endregion
            #region CandidateApplyJobSource
            CreateMap<InsertCandidateApplyJobSourceDTO, CandidateApplyJobSource>();
            CreateMap<CandidateApplyJobSourceDTO, CandidateApplyJobSource>().ReverseMap();
            CreateMap<UpdateCandidateApplyJobSourceDTO, CandidateApplyJobSource>().IgnoreAllNonExisting();
            #endregion
            #region RecruitmentCampaign
            CreateMap<InsertRecruitmentCampaignDTO, RecruitmentCampaign>();
            CreateMap<RecruitmentCampaignDTO, RecruitmentCampaign>().ReverseMap();
            CreateMap<UpdateRecruitmentCampaignDTO, RecruitmentCampaign>().IgnoreAllNonExisting();
            CreateMap<ChangeApproveRecruitmentCampaignDTO, RecruitmentCampaign>().IgnoreAllNonExisting();
            #endregion
            #region EmployerCart
            CreateMap<InsertEmployerCartDTO, EmployerCart>();
            CreateMap<EmployerCartDTO, EmployerCart>().ReverseMap();
            CreateMap<InsertEmployerCartDTO, EmployerCart>().IgnoreAllNonExisting();
            #endregion
            // JobType
            CreateMap<InsertJobTypeDTO, JobType>();
            CreateMap<JobTypeDTO, JobType>().ReverseMap();
            CreateMap<UpdateJobTypeDTO, JobType>().IgnoreAllNonExisting();

            //Job
            CreateMap<InsertJobDTO, Job>();
            CreateMap<JobDTO, Job>().ReverseMap();
            CreateMap<UpdateJobDTO, Job>().IgnoreAllNonExisting();

            CreateMap<InsertWorkplaceDTO, WorkPlace>();
            CreateMap<UpdateWorkplaceDTO, WorkPlace>().IgnoreAllNonExisting();
            CreateMap<WorkPlace, WorkplaceDTO>().ReverseMap();


            CreateMap<InsertOccupationDTO, Occupation>();
            CreateMap<UpdateOccupationDTO, Occupation>().IgnoreAllNonExisting();
            CreateMap<OccupationDTO, Occupation>().ReverseMap();

            CreateMap<SignupCandidateDTO, Candidate>();
            CreateMap<CandidateDTO, Candidate>().ReverseMap();

            CreateMap<InsertJobCategoryDTO, JobCategory>();
            CreateMap<UpdateJobCategoryDTO, JobCategory>().IgnoreAllNonExisting();
            CreateMap<JobCategoryDTO, JobCategory>().ReverseMap();

            CreateMap<InsertRecruitmentStatusDTO, RecruitmentStatus>();
            CreateMap<UpdateMultimediaTypeDTO, RecruitmentStatus>().IgnoreAllNonExisting();
            CreateMap<RecruitmentStatusDTO, RecruitmentStatus>().ReverseMap();

            CreateMap<InsertMultimediaTypeDTO, MultimediaType>();
            CreateMap<UpdateMultimediaTypeDTO, MultimediaType>().IgnoreAllNonExisting();
            CreateMap<MultimediaTypeDTO, MultimediaType>().ReverseMap();

            CreateMap<InsertOrderStatusDTO, OrderStatus>();
            CreateMap<UpdateOrderStatusDTO, OrderStatus>().IgnoreAllNonExisting();
            CreateMap<OrderStatusDTO, OrderStatus>().ReverseMap();
            
            CreateMap<InsertEmployerOrderDTO, EmployerOrder>();
            CreateMap<UpdateEmployerOrderDTO, EmployerOrder>().IgnoreAllNonExisting();
            CreateMap<EmployerOrderDTO, EmployerOrder>().ReverseMap();
            CreateMap<EmployerOrderAndOrderDetailDTO, EmployerOrder>().ReverseMap();
            CreateMap<EmployerOrderDetailDTO, EmployerOrder>().ReverseMap();
            CreateMap<UpdateInfoOrderDTO, EmployerOrder>().ReverseMap();

            CreateMap<InsertPaymentMethodDTO, PaymentMethod>();
            CreateMap<UpdatePaymentMethodDTO, PaymentMethod>().IgnoreAllNonExisting();
            CreateMap<PaymentMethodDTO, PaymentMethod>().ReverseMap();

            CreateMap<InsertInterviewStatusDTO, InterviewStatus>();
            CreateMap<UpdateInterviewStatusDTO, InterviewStatus>().IgnoreAllNonExisting();
            CreateMap<InterviewStatusDTO, InterviewStatus>().ReverseMap();

            CreateMap<InsertNotificationTypeDTO, NotificationType>();
            CreateMap<UpdateNotificationTypeDTO, NotificationType>().IgnoreAllNonExisting();    
            CreateMap<NotificationTypeDTO, NotificationType>().ReverseMap();

            CreateMap<InsertCandidateFollowCompanyDTO, CandidateFollowCompany>();

            CreateMap<InsertPostTypeDTO, PostType>();
            CreateMap<UpdatePostTypeDTO, PostType>().IgnoreAllNonExisting();
            CreateMap<PostTypeDTO, PostType>().ReverseMap();

            CreateMap<InsertPostLayoutDTO, PostLayout>();
            CreateMap<UpdatePostLayoutDTO, PostLayout>().IgnoreAllNonExisting();
            CreateMap<PostLayoutDTO, PostLayout>().ReverseMap();

            CreateMap<InsertPostStatusDTO, PostStatus>();
            CreateMap<UpdatePostStatusDTO, PostStatus>().IgnoreAllNonExisting();

            CreateMap<InsertSkillLevelDTO, SkillLevel>();
            CreateMap<UpdateSkillLevelDTO, SkillLevel>().IgnoreAllNonExisting();
            CreateMap<SkillLevelDTO, SkillLevel>().ReverseMap();

            CreateMap<InsertLicenseTypeDTO, LicenseType>();
            CreateMap<UpdateLicenseTypeDTO, LicenseType>().IgnoreAllNonExisting();
            CreateMap<LicenseTypeDTO, LicenseType>().ReverseMap();

            CreateMap<CouponDTO, Coupon>().ReverseMap();
            CreateMap<InsertCouponDTO, Coupon>();
            CreateMap<UpdateCouponDTO, Coupon>().IgnoreAllNonExisting();

            CreateMap<CouponTypeDTO, CouponType>().ReverseMap();
            CreateMap<InsertCouponTypeDTO, CouponType>();
            CreateMap<UpdateCouponTypeDTO, CouponType>().IgnoreAllNonExisting();

            CreateMap<JobPositionDTO, JobPosition>().ReverseMap();
            CreateMap<InsertJobPositionDTO, JobPosition>();
            CreateMap<UpdateJobPositionDTO, JobPosition>().IgnoreAllNonExisting();

            CreateMap<ExperienceRangeDTO, ExperienceRange>().ReverseMap();
            CreateMap<InsertExperienceRangeDTO, ExperienceRange>();
            CreateMap<UpdateExperienceRangeDTO, ExperienceRange>().IgnoreAllNonExisting();

            CreateMap<JobReferenceDTO, JobReference>().ReverseMap();
            CreateMap<InsertJobReferenceDTO, JobReference>();
            CreateMap<UpdateJobReferenceDTO, JobReference>().IgnoreAllNonExisting();

            #region jobSuitable
            CreateMap<JobSuitableDTO, JobSuitable>().ReverseMap();
            CreateMap<InsertJobSuitableDTO, JobSuitable>();
            CreateMap<UpdateJobSuitableDTO, JobSuitable>().IgnoreAllNonExisting();
            #endregion

            #region jobRequireSkill

            CreateMap<JobSkillDTO, JobSkill>().ReverseMap();
            CreateMap<InsertJobSkillDTO, JobSkill>();
            CreateMap<UpdateJobSkillDTO, JobSkill>().IgnoreAllNonExisting();
            #endregion

            CreateMap<SalaryTypeDTO, SalaryType>().ReverseMap();
            CreateMap<InsertSalaryTypeDTO, SalaryType>();
            CreateMap<UpdateSalaryTypeDTO, SalaryType>().IgnoreAllNonExisting();

            CreateMap<MenuDTO, Menu>().ReverseMap();
            CreateMap<InsertMenuDTO, Menu>();
            CreateMap<UpdateMenuDTO, Menu>().IgnoreAllNonExisting();

            CreateMap<MenuTypeDTO, MenuType>().ReverseMap();
            CreateMap<InsertMenuTypeDTO, MenuType>();
            CreateMap<UpdateMenuTypeDTO, MenuType>().IgnoreAllNonExisting();

            CreateMap<SalaryRangeDTO, SalaryRange>().ReverseMap();
            CreateMap<InsertSalaryRangeDTO, SalaryRange>();
            CreateMap<UpdateSalaryRangeDTO, SalaryRange>().IgnoreAllNonExisting();

            CreateMap<CandidateLevelDTO, CandidateLevel>().ReverseMap();
            CreateMap<InsertCandidateLevelDTO, CandidateLevel>();
            CreateMap<UpdateCandidateLevelDTO, CandidateLevel>().IgnoreAllNonExisting();

            CreateMap<AccountStatusDTO, AccountStatus>().ReverseMap();
            CreateMap<InsertAccountStatusDTO, AccountStatus>();
            CreateMap<UpdateAccountStatusDTO, AccountStatus>().IgnoreAllNonExisting();

            CreateMap<InsertJobStatusDTO, JobStatus>();
            CreateMap<JobStatusDTO, JobStatus>().ReverseMap();
            CreateMap<UpdateJobStatusDTO, JobStatus>().IgnoreAllNonExisting();

            CreateMap<ChangePasswordDTO, Employer>().IgnoreAllNonExisting();
            CreateMap<EmployerSignUpDTO, Employer>();

            CreateMap<PositionDTO, Position>().ReverseMap();
            CreateMap<InsertPositionDTO, Position>();
            CreateMap<UpdatePositionDTO, Position>();  
            CreateMap<EmployerNotificationDTO, EmployerNotification>().ReverseMap();
            #region employer
            CreateMap<UpdateEmployerDTO, Employer>().IgnoreAllNonExisting();
            CreateMap<EmployerDTO, Employer>().ReverseMap();
            CreateMap<SettingNotiEmailDTO, Candidate>().IgnoreAllNonExisting();
            CreateMap<EmployerDetailDTO, Employer>().ReverseMap();
            #endregion

            CreateMap<EmployerServicePackageDTO, EmployerServicePackage>().ReverseMap();
            CreateMap<InsertEmployerServicePackageDTO, EmployerServicePackage>().IgnoreAllNonExisting();
            CreateMap<UpdateEmployerServicePackageDTO, EmployerServicePackage>().IgnoreAllNonExisting();

            CreateMap<CandidateWorkExperienceDTO, CandidateWorkExperience>().ReverseMap();
            CreateMap<CandidateSkillDTO, CandidateSkill>().ReverseMap();
            CreateMap<CandidateEducationDTO, CandidateEducation>().ReverseMap();
            CreateMap<CandidateActivitesDTO, CandidateActivities>().ReverseMap();
            CreateMap<CandidateCertificateDTO, CandidateCertificate>().ReverseMap();
            CreateMap<CandidateHonorAwardDTO, CandidateHonorAndAward>().ReverseMap();
            CreateMap<CandidateProjectsDTO, CandidateProjects>().ReverseMap();
            CreateMap<PostStatusDTO, PostStatus>().ReverseMap();
            CreateMap<CandidateSuggestionJobCategoryDTO, CandidateSuggestionJobCategory>().ReverseMap();
            CreateMap<CandidateSuggestionJobPositionDTO, CandidateSuggestionJobPosition>().ReverseMap();
            CreateMap<CandidateSuggestionJobSkillDTO, CandidateSuggestionJobSkill>().ReverseMap();
            CreateMap<CandidateSuggestionWorkPlaceDTO, CandidateSuggestionWorkPlace>().ReverseMap();

            CreateMap<InsertPostCategoryDTO, PostCategory>();
            CreateMap<UpdatePostCategoryDTO, PostCategory>().IgnoreAllNonExisting();
            CreateMap<PostCategoryDTO, PostCategory>().ReverseMap();

            CreateMap<InsertPostDTO, Post>();
            CreateMap<UpdatePostDTO, Post>().IgnoreAllNonExisting();
            CreateMap<ApprovePostDTO, Post>().IgnoreAllNonExisting();
            CreateMap<PostDTO, Post>().ReverseMap();
            CreateMap<InsertPostTagDTO, PostTag>();
            CreateMap<UpdatePostTagDTO, PostTag>().IgnoreAllNonExisting();
            CreateMap<PostTagDTO, PostTag>().ReverseMap();

            CreateMap<InsertTagDTO, Tag>();
            CreateMap<UpdateTagDTO, Tag>().IgnoreAllNonExisting();
            CreateMap<TagDTO, Tag>().ReverseMap();

            CreateMap<InsertTagTypeDTO , TagType>();
            CreateMap<UpdateTagTypeDTO , TagType>().IgnoreAllNonExisting();
            CreateMap<TagTypeDTO , TagType>().ReverseMap();


            CreateMap<CandidateLevelDTO, CandidateLevel>().ReverseMap();
            CreateMap<InsertCandidateLevelDTO, CandidateLevel>();
            CreateMap<UpdateCandidateLevelDTO, CandidateLevel>().IgnoreAllNonExisting();

            CreateMap<AccountStatusDTO, AccountStatus>().ReverseMap();
            CreateMap<InsertAccountStatusDTO, AccountStatus>();
            CreateMap<UpdateAccountStatusDTO, AccountStatus>().IgnoreAllNonExisting();

            CreateMap<InsertJobStatusDTO, JobStatus>();
            CreateMap<JobStatusDTO, JobStatus>().ReverseMap();
            CreateMap<UpdateJobStatusDTO, JobStatus>().IgnoreAllNonExisting();

            CreateMap<ChangePasswordDTO, Employer>().IgnoreAllNonExisting();
            CreateMap<EmployerSignUpDTO, Employer>();

            CreateMap<PositionDTO, Position>().ReverseMap();
            CreateMap<UpdateProfileCandidateDTO, Candidate>();
            CreateMap<CandidateWorkExperienceDTO, CandidateWorkExperience>();
            CreateMap<SettingNotiEmailDTO, Candidate>().IgnoreAllNonExisting();

            //employer benefit
            CreateMap<EmployerBenefitDTO, EmployerBenefit>().ReverseMap();
            CreateMap<InsertEmployerBenefitDTO, EmployerBenefit>().IgnoreAllNonExisting();
            CreateMap<UpdateEmployerBenefitDTO, EmployerBenefit>().IgnoreAllNonExisting();

            //employer service package employer benefit
            CreateMap<EmployerServicePackageEmployerBenefitDTO, EmployerServicePackageEmployerBenefit>().ReverseMap();
            CreateMap<InsertEmployerServicePackageEmployerBenefitDTO, EmployerServicePackageEmployerBenefit>().IgnoreAllNonExisting();
            CreateMap<UpdateEmployerServicePackageEmployerBenefitDTO, EmployerServicePackageEmployerBenefit>().IgnoreAllNonExisting();

            //company
            CreateMap<UpdateCompanyDTO, Company>().IgnoreAllNonExisting();
            CreateMap<CompanyDTO, Company>().ReverseMap();
            CreateMap<InsertCompanyDTO, Company>();

            #region company license
            CreateMap<UpdateLicenseDTO, License>().IgnoreAllNonExisting();
            CreateMap<ApproveLicenseDTO, License>().IgnoreAllNonExisting();
            CreateMap<LicenseDTO, License>().ReverseMap();
            CreateMap<InsertLicenseDTO, License>();
            #endregion
            CreateMap<CompanyAdminDTO, Company>().ReverseMap();
            CreateMap<CompanySizeDTO, CompanySize>().ReverseMap();
            CreateMap<InsertCompanySizeDTO, CompanySize>().IgnoreAllNonExisting();
            CreateMap<UpdateCompanySizeDTO, CompanySize>().IgnoreAllNonExisting();

            CreateMap<UpdateCandidateSaveJobDTO, CandidateSaveJob>().IgnoreAllNonExisting();
            CreateMap<InsertCandidateSaveJobDTO, CandidateSaveJob>().IgnoreAllNonExisting();
            CreateMap<CandidateSaveJobDTO, CandidateSaveJob>().ReverseMap();

            ///Topcompany
            CreateMap<TopCompany, TopCompanyDTO>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<TopCompanyDTO, TopCompany>();
            CreateMap<InsertTopCompanyDTO, TopCompany>().IgnoreAllNonExisting();
            CreateMap<UpdateTopCompanyDTO, TopCompany>().IgnoreAllNonExisting();
            

            ///Skill
            CreateMap<InsertSkillDTO, Skill>();
            CreateMap<UpdateSkillDTO, Skill>().IgnoreAllNonExisting();
            CreateMap<SkillDTO, Skill>().ReverseMap();

            #region job
            CreateMap<InsertJobDTO, Job>().IgnoreAllNonExisting();
            CreateMap<UpdateJobDTO, Job>().IgnoreAllNonExisting();
            CreateMap<JobDetailAggregates, Job>().IgnoreAllNonExisting();
            CreateMap<JobDetailDTO, Job>().ReverseMap();

            #endregion

            #region jobRequireCity
            CreateMap<InsertJobRequireCityDTO, JobRequireCity>().IgnoreAllNonExisting();
            CreateMap<UpdateJobRequireCityDTO, JobRequireCity>().IgnoreAllNonExisting();
            CreateMap<DetailJobRequireCityDTO, JobRequireCity>().ReverseMap();
            CreateMap<JobWorkPlaceDTO, JobRequireCity>().IgnoreAllNonExisting();
            #endregion

            #region jobRequireDistrict
            CreateMap<JobWorkPlaceDTO, JobRequireDistrict>().IgnoreAllNonExisting();
            #endregion

            #region jobSecondaryPosition
            CreateMap<InsertJobSecondaryJobCategoryDTO, JobSecondaryJobCategory>().IgnoreAllNonExisting();
            CreateMap<UpdateJobSecondaryJobCategoryDTO, JobSecondaryJobCategory>().IgnoreAllNonExisting();
            CreateMap<JobSecondaryJobCategoryDTO, JobSecondaryJobCategory>().IgnoreAllNonExisting();
            CreateMap<DetailJobSecondaryJobCategoryDTO, JobSecondaryJobCategory>().ReverseMap();
            #endregion

            ///TopFeatureJob
            CreateMap<TopFeatureJob, TopFeatureJobDTO>()
            .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.Name));
            CreateMap<TopFeatureJobDTO, TopFeatureJob>(); 

            CreateMap<InsertTopFeatureJobDTO, TopFeatureJob>().IgnoreAllNonExisting();
            CreateMap<UpdateTopFeatureJobDTO, TopFeatureJob>().IgnoreAllNonExisting();


            CreateMap<CandidateCVPDFDTO, CandidateCVPDF>().ReverseMap();
            CreateMap<UploadCandidateCVPDFDTO, CandidateCVPDF>().ReverseMap();
            CreateMap<InsertCandidateCVPDFDTO, CandidateCVPDF>();
            CreateMap<UpdateCandidateCVPDFDTO, CandidateCVPDF>().IgnoreAllNonExisting();

            CreateMap<CandidateCVPDFTypesDTO, CandidateCVPDFType>().ReverseMap();
            CreateMap<InsertCandidateCVPDFTypesDTO, CandidateCVPDFType>();
            CreateMap<UpdateCandidateCVPDFTypesDTO, CandidateCVPDFType>().IgnoreAllNonExisting();

            #region ServicePackage
            CreateMap<BenefitDTO, Benefit>().ReverseMap();
            CreateMap<InsertBenefitDTO, Benefit>();
            CreateMap<UpdateBenefitDTO, Benefit>().IgnoreAllNonExisting();


            CreateMap<ServicePackageGroupDTO, ServicePackageGroup>().ReverseMap();
            CreateMap<InsertServicePackageGroupDTO, ServicePackageGroup>();
            CreateMap<UpdateServicePackageGroupDTO, ServicePackageGroup>().IgnoreAllNonExisting();

            CreateMap<ServicePackageTypeDTO, ServicePackageType>().ReverseMap();
            CreateMap<InsertServicePackageTypeDTO, ServicePackageType>();
            CreateMap<UpdateServicePackageTypeDTO, ServicePackageType>().IgnoreAllNonExisting();

            CreateMap<ServicePackageBenefitDTO, ServicePackageBenefit>().ReverseMap();
            CreateMap<InsertServicePackageBenefitDTO, ServicePackageBenefit>();
            CreateMap<UpdateServicePackageBenefitDTO, ServicePackageBenefit>().IgnoreAllNonExisting();
            #endregion
            CreateMap<TopJobManagementDTO, TopJobManagement>().ReverseMap();
            CreateMap<InsertTopJobManagementDTO, TopJobManagement>();
            CreateMap<UpdateTopJobManagementDTO, TopJobManagement>().IgnoreAllNonExisting();

            CreateMap<MustBeInterestedCompanyDTO, MustBeInterestedCompany>().ReverseMap();
            CreateMap<InsertMustBeInterestedCompanyDTO, MustBeInterestedCompany>();
            CreateMap<UpdateMustBeInterestedCompanyDTO, MustBeInterestedCompany>().IgnoreAllNonExisting();

            CreateMap<TopJobExtraDTO, TopJobExtra>().ReverseMap();
            CreateMap<InsertTopJobExtraDTO, TopJobExtra>();
            CreateMap<UpdateTopJobExtraDTO, TopJobExtra>().IgnoreAllNonExisting();

            CreateMap<TopJobUrgentDTO, TopJobUrgent>().ReverseMap();
            CreateMap<InsertTopJobUrgentDTO, TopJobUrgent>();
            CreateMap<UpdateTopJobUrgentDTO, TopJobUrgent>().IgnoreAllNonExisting();

            #region Cart
            CreateMap<EmployerCartDTO, EmployerCart>().ReverseMap();
            CreateMap<InsertEmployerCartDTO, EmployerCart>();
            CreateMap<UpdateEmployerCartDTO, EmployerCart>().IgnoreAllNonExisting();

            CreateMap<CreateEmployerOrderDTO, EmployerOrder>().IgnoreAllNonExisting();
            CreateMap<CreateEmployerOrderDetailDTO, EmployerOrderDetail>().IgnoreAllNonExisting();

            #endregion
            // CandidateCV
            CreateMap<InsertOrUpdateCandidateCVDTO, CandidateCV>().IgnoreAllNonExisting();

            //EmployerWallet
            CreateMap<EmployerWalletDTO, EmployerWallet>().ReverseMap();
            CreateMap<InsertEmployerWalletDTO, EmployerWallet>().IgnoreAllNonExisting();
            CreateMap<UpdateEmployerWalletDTO, EmployerWallet>().IgnoreAllNonExisting();

            CreateMap<EmployerWalletHistoryDTO, EmployerWalletHistory>().ReverseMap();
            CreateMap<InsertEmployerWalletHistoryDTO, EmployerWalletHistory>().IgnoreAllNonExisting();
            CreateMap<UpdateEmployerWalletHistoryDTO, EmployerWalletHistory>().IgnoreAllNonExisting();
        }
    }
}
