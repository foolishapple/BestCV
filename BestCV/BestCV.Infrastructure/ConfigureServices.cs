using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Core.Repositories;
using Microsoft.AspNetCore.Routing;
using BestCV.Domain.Entities;

namespace BestCV.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobiContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
                builder => builder.MigrationsAssembly(typeof(JobiContext).Assembly.FullName)));

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
                .AddScoped<ICandidateRepository, CandidateRepository>()
                .AddScoped<ICandidateMetaRepository, CandidateMetaRepository>()
                .AddScoped<IJobCategoryRepository, JobCategoryRepository>()
                .AddScoped<IRecruitmentStatusRepository, RecruitmentStatusRepository>()
                .AddScoped<IMultimediaTypeRepository, MultimediaTypeRepository>()
                .AddScoped<IOrderStatusRepository, OrderStatusRepository>()
                .AddScoped<IEmployerOrderRepository, EmployerOrderRepository>()
                .AddScoped<IPaymentMethodRepository, PaymentMethodRepository>()
                .AddScoped<INotificationTypeRepository, NotificationTypeRepository>()
                .AddScoped<IPostTypeRepository, PostTypeRepository>()
                .AddScoped<IPostLayoutRepository, PostLayoutRepository>()
                .AddScoped<IPostStatusRepository, PostStatusRepository>()
                .AddScoped<IInterviewStatusRepository, InterviewStatusRepository>()
                .AddScoped<IWorkplaceRepository, WorkplaceRepository>()
                .AddScoped<IInterviewStatusRepository, InterviewStatusRepository>()
                .AddScoped<ISalaryTypeRepository, SalaryTypeRepository>()
                .AddScoped<ICouponTypeRepository, CouponTypeRepository>()
                .AddScoped<IExperienceRangeRepository, ExperienceRangeRepository>()
                .AddScoped<IJobPositionRepository, JobPositionRepository>()
                .AddScoped<IJobSuitableRepository, JobSuitableRepository>()
                .AddScoped<IJobSkillRepository, JobSkillRepository>()
                .AddScoped<ISalaryRangeRepository, SalaryRangeRepository>()
                .AddScoped<ICandidateLevelRepository, CandidateLevelRepository>()
                .AddScoped<ICandidateCVPDFTypesRepository, CandidateCVPDFTypesRepository>()
                .AddScoped<IAccountStatusRepository, AccountStatusRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IMenuTypeRepository, MenuTypeRepository>()
                .AddScoped<ICouponRepository, CouponRepository>()
                .AddScoped<IAdminAccountRepository, AdminAccountRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IPermissionRepository, PermissionRepository>()
                .AddScoped<IFolderUploadRepository, FolderUploadRepository>()
                .AddScoped<IUploadFileRepository, UploadFileRepository>()
                .AddScoped<ICouponRepository, CouponRepository>()
                .AddScoped<IPositionRepository, PositionRepository>()
                .AddScoped<IEmployerRepository, EmployerRepository>()
                .AddScoped<ICouponRepository, CouponRepository>()
                .AddScoped<ICandidateRepository, CandidateRepository>()
                .AddScoped<ICandidateMetaRepository, CandidateMetaRepository>()
                .AddScoped<IJobTypeRepository, JobTypeRepository>()
                .AddScoped<IPostCategoryRepository, PostCategoryRepository>()
                .AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IInterviewStatusRepository, InterviewStatusRepository>()
                .AddScoped<IEmployerRepository, EmployerRepository>()
                .AddScoped<ICandidateApplyJobStatusRepository, CandidateApplyJobStatusRepository>()
                .AddScoped<ICompanyRepository, CompanyRepository>()
                .AddScoped<ICompanySizeRepository, CompanySizeRepository>()
                .AddScoped<ILicenseRepository, LicenseRepository>()
                .AddScoped<ICouponRepository, CouponRepository>()
                .AddScoped<IAdminAccountRoleRepository, AdminAccountRoleRepository>()
                .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
                .AddScoped<IRoleMenuRepository, RoleMenuRepository>()
                .AddScoped<IEmployerServicePackageRepository, EmployerServicePackageRepository>()
                .AddScoped<ISystemConfigRepository, SystemConfigRepository>()
                .AddScoped<ISlideRepository, SlideRepository>()
                .AddScoped<INotificationStatusRepository, NotificationStatusRepository>()
                .AddScoped<IInterviewTypeRepository, InterviewTypeRepository>()
                .AddScoped<IOrderTypeRepository, OrderTypeRepository>()
                .AddScoped<IVoucherTypeRepository, VoucherTypeRepository>()
                .AddScoped<IEmployerActivityLogTypeRepository, EmployerActivityLogTypeRepository>()
                .AddScoped<IRecruitmentCampaignStatusRepository, RecruitmentCampaignStatusRepository>()
                .AddScoped<IFieldOfActivityRepository, FieldOfActivityRepository>()
                .AddScoped<ICompanyFieldOfActivityRepository, CompanyFieldOfActivityRepository>()
                .AddScoped<ICandidateApplyJobRepository, CandidateApplyJobRepository>()
                .AddScoped<ICandidateApplyJobSourceRepository, CandidateApplyJobSourceRepository>()
                .AddScoped<IRecruitmentCampaignRepository, RecruitmentCampaignRepository>();

            services.AddScoped<ICandidateCVPDFRepository, CandidateCVPDFRepository>();
            // CV module start
            services.AddScoped<ICVTemplateRepository, CVTemplateRepository>();
            services.AddScoped<ICandidateCVRepository, CandidateCVRepository>();
            // CV module end
            services.AddScoped<ICandidateSaveJobRepository, CandidateSaveJobRepository>();
            services.AddScoped<ICandidateViewedJobRepository, CandidateViewedJobRepository>();
            services.AddScoped<ICandidateFollowCompanyRepository, CandidateFollowCompanyRepository>();
            services.AddScoped<IOccupationRepository, OccupationRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateMetaRepository, CandidateMetaRepository>();
            services.AddScoped<IJobTypeRepository, JobTypeRepository>();
            services.AddScoped<IJobStatusRepository, JobStatusRepository>();
            services.AddScoped<ICandidateNotificationRepository, CandidateNotificationRepository>();
            services.AddScoped<IAdminAccountRepository, AdminAccountRepository>();
            services.AddScoped<IAdminAccountMetaRepository, AdminAccountMetaRepository>();
            services.AddScoped<ICandidateWorkExperienceRepository, CandidateWorkExperienceRepository>();
            services.AddScoped<ICandidateEducationRepository, CandidateEducationRepository>();
            services.AddScoped<ICandidateSkillRepository, CandidateSkillRepository>();
            services.AddScoped<ICandidateActivitesRepository, CandidateActivitesRepository>();
            services.AddScoped<ICandidateCertificateRepository, CandidateCertificateRepository>();
            services.AddScoped<ICandidateHonorAwardRepository, CandidateHonorAwardRepository>();
            services.AddScoped<ICandidateProjectsRepository, CandidateProjectsRepository>();
            services.AddScoped<ISkillLevelRepository, SkillLevelRepository>();
            services.AddScoped<ILicenseTypeRepository, LicenseTypeRepository>();
            services.AddScoped<IEmployerBenefitRepository, EmployerBenefitRepository>();
            services.AddScoped<ICandidateViewedJobRepository, CandidateViewedJobRepository>();
            services.AddScoped<IEmployerServicePackageEmployerBenefitRepository, EmployerServicePackageEmployerBenefitRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            services.AddScoped<IEmployerRepository, EmployerRepository>();
            services.AddScoped<IEmployerMetaRepository, EmployerMetaRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IJobTypeRepository, JobTypeRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagtypeRepository, TagTypeRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<ICandidateSuggestionJobCategoryRepository, CandidateSuggestionJobCategoryRepository>();
            services.AddScoped<ICandidateSuggestionJobPositionRepository, CandidateSuggestionJobPositionRepository>();
            services.AddScoped<ICandidateSuggestionJobSkillRepository, CandidateSuggestionJobSkillRepository>();
            services.AddScoped<ICandidateSuggestionWorkPlaceRepository, CandidateSuggestionWorkPlaceRepository>();
            services.AddScoped<IEmployerNotificationRepository, EmployerNotificationRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobSecondaryJobCategoryRepository, JobSecondaryJobCategoryRepository>();
            services.AddScoped<IJobRequireJobSkillRepository, JobRequireJobSkillRepository>();
            services.AddScoped<IJobRequireCityRepository, JobRequireCityRepository>();
            services.AddScoped<IJobRequireDistrictRepository, JobRequireDistrictRepository>();
            services.AddScoped<IJobTagRepository, JobTagRepository>();
            services.AddScoped<IJobReasonApplyRepository, JobReasonApplyRepository>();
            services.AddScoped<ITopCompanyRepository, TopCompanyRepository>();
            services.AddScoped<ITopFeatureJobRepository, TopFeatureJobRepository>();
            services.AddScoped<IInterviewScheduleRepository, InterviewScheduleRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IBenefitRepository, BenefitRepository>();
            services.AddScoped<IServicePackageTypeRepository, ServicePackageTypeRepository>();
            services.AddScoped<IServicePackageGroupRepository, ServicePackageGroupRepository>();
            services.AddScoped<IServicePackageBenefitRepository, ServicePackageBenefitRepository>();
            services.AddScoped<ITopJobExtraRepository, TopJobExtraRepository>();
            services.AddScoped<ITopJobUrgentRepository, TopJobUrgentRepository>();
            services.AddScoped<ITopJobManagementRepository, TopJobManagementRepository>();
            services.AddScoped<IMustBeInterestedCompanyRepository, MustBeInterestedCompanyRepository>();
            services.AddScoped<IEmployerWalletRepository, EmployerWalletRepository>();
            services.AddScoped<IWalletTypeRepository, WalletTypeRepository>();
            services.AddScoped<IJobServicePackageRepository, JobServicePackageRepository>();
            services.AddScoped<IEmployerServicePackageEmployerRepository, EmployerServicePackageEmployerRepository>();
            services.AddScoped<IEmployerCartRepository, EmployerCartRepository>();
            services.AddScoped<IEmployerOrderDetailRepository, EmployerOrderDetailRepository>();
            services.AddScoped<IJobReferenceRepository, JobReferenceRepository>();
            services.AddScoped<ITopAreaJobRepository, TopAreaJobRepository>();
            services.AddScoped<IEmployerWalletHistoriesRepository, EmployerWalletHistoriesRepository>();
            services.AddScoped<IRefreshJobRepository, RefreshJobRepository>();
            return services;

        }
    }
}
