using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using Jobi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class Employer : EntityBase<long>, IFullTextSearch
    {
        public bool IsActivated { get; set; }
        /// <summary>
        /// Mã trạng thái nhà tuyển dụng		
        /// </summary>
        public int EmployerStatusId { get; set; }
        /// <summary>
        /// Thời gian hết hạn gói dịch vụ
        /// </summary>
        public DateTime? EmployerServicePackageEfficiencyExpiry { get; set; }
        /// <summary>
        /// Mã chức vụ nhà tuyển dụng
        /// </summary>
        public int PositionId { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Photo { get; set; }
        public int Gender { get; set; }
        public string? Phone { get; set; }

        public int? AccessFailedCount { get; set; }
        public bool? LockEnabled { get; set; }
        public DateTime? LockEndDate { get; set; }

        public string? SkypeAccount { get; set; }

        public string Search { get; set; } = null!;
        public string? Description { get; set; }

        public virtual AccountStatus EmployerStatus { get; set; } = null!;

        public virtual Position Position { get; set; } = null!;

        public virtual Company Company { get; }

        public virtual ICollection<EmployerPassword> EmployerPasswords { get; } = new List<EmployerPassword>();

        public virtual ICollection<EmployerVoucher> EmployerVouchers { get; } = new List<EmployerVoucher>();

        public virtual ICollection<EmployerNotification> EmployerNotifications { get; } = new List<EmployerNotification>();

        public virtual ICollection<EmployerViewedCV> EmployerViewedCVs { get; set; } = new List<EmployerViewedCV>();

        public virtual ICollection<EmployerMeta> EmployerMetas { get; } = new List<EmployerMeta>();
        public virtual ICollection<EmployerOrder> EmployerOrders { get; } = new List<EmployerOrder>();

        public virtual ICollection<RecruitmentCampaign> RecruitmentCampaigns { get; } = new List<RecruitmentCampaign>();
        public virtual ICollection<EmployerWallet> EmployerWallets { get; } = new List<EmployerWallet>();
        public virtual ICollection<EmployerCart> EmployerCarts { get; } = new List<EmployerCart>();
    }
}
