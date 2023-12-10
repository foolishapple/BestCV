using Microsoft.Extensions.DependencyInjection;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;
using FluentValidation.AspNetCore;
using FluentValidation;
using BestCV.Infrastructure.Repositories.Interfaces;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Domain.Entities;

namespace BestCV.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
            //Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISalaryTypeService, SalaryTypeService>();
            services.AddScoped<ICouponTypeService, CouponTypeService>();
            services.AddScoped<IExperienceRangeService, ExperienceRangeService>();
            services.AddScoped<IJobPositionService, JobPositionService>();
            services.AddScoped<IJobSkillService, JobSkillService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuTypeService, MenuTypeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkplaceService, WorkplaceService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICandidateFollowCompanyService, CandidateFollowCompanyService>();
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IJobSuitableService, JobSuitableService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<IRecruitmentStatusService, RecruitmentStatusService>();
            services.AddScoped<IMultimediaTypeService, MultimediaTypeService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IEmployerOrderService, EmployerOrderService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IInterviewStatusService, InterviewStatusService>();
            services.AddScoped<IInterviewTypeService, InterviewTypeService>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();
            services.AddScoped<INotificationStatusService, NotificationStatusService>();
            services.AddScoped<IPostTypeService, PostTypeService>();
            services.AddScoped<IPostLayoutService, PostLayoutService>();
            services.AddScoped<IPostStatusService, PostStatusService>();
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IJobTypeService, JobTypeService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICandidateLevelService, CandidateLevelService>();
            services.AddScoped<IAccountStatusService, AccountStatusService>();
            services.AddScoped<ISalaryRangeService, SalaryRangeService>();
            services.AddScoped<ILicenseTypeService, LicenseTypeService>();
            services.AddScoped<IEmployerServicePackageService, EmployerServicePackageService>();
            services.AddScoped<ICompanySizeService, CompanySizeService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IEmployerBenefitService, EmployerBenefitService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<ICandidateNotificationService,CandidateNotificationService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IFileExplorerService, FileExplorerService>();
            services.AddScoped<IOccupationService, OccupationService>();
            services.AddScoped<ISkillLevelService, SkillLevelService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagTypeService, TagTypeService>();
            services.AddScoped<IPostTagService, PostTagService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<ITopCompanyService, TopCompanyService>();
            services.AddScoped<ITopFeatureJobService, TopFeatureJobService>();
            services.AddScoped<IEmployerNotificationService, EmployerNotificationService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IFieldOfActivityService, FieldOfActivityService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<ICandidateSaveJobService, CandidateSaveJobService>();
            services.AddScoped<ICandidateHomeService, CandidateHomeService>();
            services.AddScoped<IJobSecondaryJobPositionService, JobSecondaryJobCategoryService>();
            services.AddScoped<IRecruitmentCampaignStatusService, RecruitmentCampaignStatusService>();
            services.AddScoped<ICandidateCVPDFTypesService, CandidateCVPDFTypesService>();
            services.AddScoped<ICandidateCVPDFService, CandidateCVPDFService>();

            // CV module start
            services.AddScoped<ICVTemplateService, CVTemplateService>();
            services.AddScoped<ICandidateCVService, CandidateCVService>();
            // CV module end

            services.AddScoped<ICompanyFieldOfActivityService, CompanyFieldOfActivityService>();
            services.AddScoped<IJobStatusService, JobStatusService>();

            services.AddScoped<IEmployerServicePackageEmployerBenefitService, EmployerServicePackageEmployerBenefitService>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<ICandidateApplyJobService, CandidateApplyJobService>();
            services.AddScoped<ICandidateApplyJobStatusService, CandidateApplyJobStatusService>();
            services.AddScoped<ICandidateApplyJobSourceService, CandidateApplyJobSourceService>();
            services.AddScoped<IRecruitmentCampaignService, RecruitmentCampaignService>();
            services.AddScoped<ICandidateViewedJobService, CandidateViewedJobService>();
            services.AddScoped<IInterviewScheduleService, InterviewScheduleService>();
            services.AddScoped<IOrderTypeService, OrderTypeService>();
            services.AddScoped<IBenefitService, BenefitService>();
            services.AddScoped<IServicePackageGroupService, ServicePackageGroupService>();
            services.AddScoped<IServicePackageTypeService, ServicePackageTypeService>();
            services.AddScoped<IServicePackageBenefitService, ServicePackageBenefitService>();
            services.AddScoped<ITopJobExtraService, TopJobExtraService>();
            services.AddScoped<ITopJobUrgentService, TopJobUrgentService>();
            services.AddScoped<ITopJobManagementService, TopJobManagementService>();
            services.AddScoped<IMustBeInterestedCompanyService, MustBeInterestedCompanyService>();
            services.AddScoped<IJobServicePackageService, JobServicePackageService>();
            services.AddScoped<IEmployerServicePackageEmployerService, EmployerServicePackageEmployerService>();
            services.AddScoped<IEmployerCartService, EmployerCartService>();
            services.AddScoped<IJobReferenceService, JobReferenceService>();
            services.AddScoped<IEmployerWalletService, EmployerWalletService>();
            services.AddScoped<IEmployerWalletHistoryService, EmployerWalletHistoryService>();
            //Setting

            services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMemoryCache();
            

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
