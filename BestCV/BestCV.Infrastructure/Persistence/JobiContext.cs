using Microsoft.EntityFrameworkCore;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BestCV.Infrastructure.Persistence
{
    public class JobiContext : DbContext
    {
        public JobiContext(DbContextOptions<JobiContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //SERVER LIVE: Data Source=34.71.43.185;Initial Catalog=Dion.Jobi;Persist Security Info=True;User ID=jobidev;Password=Dion@2023!@#;
        //        //SERVER TEST: Data Source=103.214.9.237;Initial Catalog=Dion.Jobi;User ID=BestCV.Dev;Password=AjUxDXnz6JBZfiI
        //        optionsBuilder.UseSqlServer(@"Data Source=103.214.9.237;Initial Catalog=Dion.Jobi;User ID=BestCV.Dev;Password=AjUxDXnz6JBZfiI");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        public virtual DbSet<RecruitmentCampaign> RecruitmentCampaigns { get; set; }
        public virtual DbSet<RecruitmentCampaignRequireJobPosition> RecruitmentCampaignRequireJobPositions { get; set; }
        public virtual DbSet<RecruitmentCampaignRequireWorkPlace> RecruitmentCampaignRequireWorkPlaces { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobSecondaryJobCategory> JobSecondaryJobCategories { get; set; }
        public virtual DbSet<JobReasonApply> JobReasonApplies { get; set; }
        public virtual DbSet<JobMultimedia> JobMultimedias { get; set; }
        public virtual DbSet<JobRequireJobSkill> JobRequireJobSkills { get; set; }
        public virtual DbSet<JobRequireCity> JobRequireCities { get; set; }
        public virtual DbSet<JobRequireDistrict> JobRequireDistricts { get; set; }
        public virtual DbSet<AdminAccountMeta> AdminAccountMetas { get; set; }
        public virtual DbSet<AdminAccountRole> AdminAccountRoles { get; set; }
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<FolderUpload> FolderUploads { get; set; }
        public virtual DbSet<UploadFile> UploadFiles { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<CouponType> CouponType { get; set; }
        public virtual DbSet<ExperienceRange> ExperienceRange { get; set; }
        public virtual DbSet<SalaryRange> SalaryRange { get; set; }
        public virtual DbSet<SalaryType> SalaryType { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<JobPosition> JobPosition { get; set; }
        public virtual DbSet<JobSkill> JobSkill { get; set; }
        public virtual DbSet<CandidateCertificate> CandidateCertificates { get; set; }
        public virtual DbSet<CandidateHonorAndAward> CandidateHonorAndAwards { get; set; }
        public virtual DbSet<CandidateActivities> CandidateActivities { get; set; }
        public virtual DbSet<CandidateProjects> CandidateProjects { get; set; }
        public virtual DbSet<CandidateSaveJob> CandidateSaveJobs { get; set; }
        public virtual DbSet<CandidateIgnoreJob> CandidateIgnoreJobs { get; set; }
        public virtual DbSet<CandidateApplyJob> CandidateApplyJobs { get; set; }
        public virtual DbSet<EmployerOrder> EmployerOrders { get; set; }
        public virtual DbSet<EmployerVoucher> EmployerVouchers { get; set; }
        public virtual DbSet<EmployerOrderDetail> EmployerOrderDetails { get; set; }
        public virtual DbSet<EmployerOrderVoucher> EmployerOrderVouchers { get; set; }
        public virtual DbSet<EmployerNotification> EmployerNotifications { get; set; }
        public virtual DbSet<EmployerViewedCV> EmployerViewedCVs { get; set; }
        public virtual DbSet<WorkPlace> WorkPlaces { get; set; }
        public virtual DbSet<TopCompany> TopCompanies { get; set; }
        public virtual DbSet<TopFeatureJob> TopFeatureJobs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<PostLayout> PostLayouts { get; set; }
        public virtual DbSet<PostStatus> PostStatuses { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public virtual DbSet<InterviewStatus> InterviewStatuses { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<MultimediaType> MultimediaTypes { get; set; }
        public virtual DbSet<RecruitmentStatus> RecruitmentStatuses { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }
        public virtual DbSet<CandidateLevel> CandidateLevels { get; set; }
        public virtual DbSet<CandidateLevelBenefit> CandidateLevelBenefits { get; set; }
        public virtual DbSet<CandidateLevelCandidateLevelBenefit> CandidateLevelCandidateLevelBenefits { get; set; }
        public virtual DbSet<CandidateMeta> CandidateMetas { get; set; }
        public virtual DbSet<CandidateApplyJobSource> CandidateApplyJobSources { get; set; }
        public virtual DbSet<CandidateViewedJob> CandidateViewedJobs { get; set; }
        public virtual DbSet<CandidateOrders> CandidateOrders { get; set; }
        public virtual DbSet<CandidateCoupon> CandidateCoupons { get; set; }
        public virtual DbSet<CandidatePassword> CandidatePasswords { get; set; }
        public virtual DbSet<CandidateSuggestionWorkPlace> CandidateSuggestionWorkPlaces { get; set; }
        public virtual DbSet<CandidateSuggestionJobPosition> CandidateSuggestionJobPositions { get; set; }
        public virtual DbSet<CandidateOrderDetail> CandidateOrderDetails { get; set; }
        public virtual DbSet<CandidateOrderCoupon> CandidateOrderCoupons { get; set; }
        public virtual DbSet<CandidateNotification> CandidateNotifications { get; set; }
        public virtual DbSet<AdminAccount> AdminAccounts { get; set; }
        public virtual DbSet<CandidateApplyJobStatus> CandidateApplyJobStatuses { get; set; }
        public virtual DbSet<EmployerActivityLog> EmployerActivityLogs { get; set; }
        public virtual DbSet<EmployerActivityLogType> EmployerActivityLogTypes { get; set; }
        public virtual DbSet<InterviewType> InterviewTypes { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<RecruitmentCampaignStatus> RecruitmentCampaignStatuses { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagType> TagTypes { get; set; }
        public virtual DbSet<VoucherType> VoucherTypes { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateSkill> CandidateSkills { get; set; }
        public virtual DbSet<CandidateWorkExperience> CandidateWorkExperiences { get; set; }
        public virtual DbSet<CandidateEducation> CandidateEducations { get; set; }
        public virtual DbSet<CandidateSuggestionJobCategory> CandidateSuggestionJobCategories { get; set; }
        public virtual DbSet<CandidateSuggestionJobSkill> CandidateSuggestionJobSkills { get; set; }
        public virtual DbSet<CandidateFollowCompany> CandidateFollowCompanies { get; set; }
        // Nhà tuyển dụng
        public virtual DbSet<FieldOfActivity> FieldOfActivities { get; set; }
        public virtual DbSet<CompanySize> CompanySizes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<EmployerPassword> EmployerPasswords { get; set; }
        public virtual DbSet<EmployerMeta> EmployerMetas { get; set; }
        public virtual DbSet<EmployerServicePackage> EmployerServicePackages { get; set; }
        public virtual DbSet<EmployerBenefit> EmployerBenefits { get; set; }
        public virtual DbSet<EmployerServicePackageEmployerBenefit> EmployerServicePackageEmployerBenefits { get; set; }
        public virtual DbSet<CompanyFieldOfActivity> CompanyFieldOfActivities { get; set; }
        public virtual DbSet<CompanyReview> CompanyReviews { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<JobTag> JobTags { get; set; }

        // CV
        public virtual DbSet<CVTemplateStatus> CVTemplateStatuses { get; set; }
        public virtual DbSet<CVTemplate> CVTemplates { get; set; }
        public virtual DbSet<CandidateCV> CandidateCVs { get; set; }
        public virtual DbSet<CandidateCVPDF> CandidateCVPDFs { get; set; }
        public virtual DbSet<CandidateCVPDFType> CandidateCVPDFTypes { get; set; }
        //Service Benefit
        public virtual DbSet<TopJobManagement> TopJobManagements { get; set; }
        public virtual DbSet<JobReference> JobReferences { get; set; }
        public virtual DbSet<MustBeInterestedCompany> MustBeInterestedCompanies { get; set; }
        public virtual DbSet<JobSuitable> JobSuitables { get; set; }
        public virtual DbSet<TopJobUrgent> TopJobUrgents { get; set; }
        public virtual DbSet<TopJobExtra> TopJobExtras { get; set; }
        public virtual DbSet<EmployerWallet> EmployerWallets { get; set; }
        public virtual DbSet<WalletType> WalletTypes { get; set; }
        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<ServicePackageBenefit> ServicePackageBenefits { get; set; }
        public virtual DbSet<ServicePackageGroup> ServicePackageGroups { get; set; }
        public virtual DbSet<ServicePackageType> ServicePackageTypes { get; set; }
        public virtual DbSet<JobServicePackage> JobServicePackages { get; set; }
        public virtual DbSet<EmployerCart> EmployerCarts { get; set; }
        public virtual DbSet<EmployerServicePackageEmployer> EmployerServicePackageEmployers { get; set; }
        public virtual DbSet<WalletHistoryType> WalletHistoryTypes { get; set; }
        public virtual DbSet<EmployerWalletHistory> EmployerWalletHistories { get; set; }
        public virtual DbSet<TopAreaJob> TopAreaJobs { get; set; }
        /// <summary>
        /// Tin tuyển dụng làm mới
        /// </summary>
        public virtual DbSet<RefreshJob> RefreshJobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(CustomQuery).GetMethod(nameof(CustomQuery.ToCustomString))).HasTranslation(
            e =>
            {
                return new SqlFunctionExpression(functionName: "format", arguments: new[]{
                            e.First(),
                            new SqlFragmentExpression("'dd/MM/yyyy HH:mm:ss'")
                        }, nullable: true, new List<bool>(), type: typeof(string), typeMapping: new StringTypeMapping("", DbType.String));
            });

            modelBuilder.HasDbFunction(typeof(CustomQuery).GetMethod(nameof(CustomQuery.ToDateString))).HasTranslation(
                e =>
                {
                    return new SqlFunctionExpression(functionName: "format", arguments: new[]{
                            e.First(),
                            new SqlFragmentExpression("'dd/MM/yyyy'")
                            }, nullable: true, new List<bool>(), type: typeof(string), typeMapping: new StringTypeMapping("", DbType.String));
                });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
