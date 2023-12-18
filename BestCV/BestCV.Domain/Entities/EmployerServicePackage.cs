using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Gói dịch vụ dành cho nhà tuyển dụng
    /// </summary>
    public class EmployerServicePackage : EntityCommon<int>
    {
        /// <summary>
        /// giá gói dịch vụ		
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// giảm giá
        /// </summary>
        public int DiscountPrice { get; set; }

        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime? DiscountEndDate { get; set; }

        ///// <summary>
        ///// Thời gian trước khi hết hạn
        ///// </summary>
        //public int ExpiryTime { get; set; }
        /// <summary>
        /// Mã Nhóm gói dịch vụ
        /// </summary>
        public int ServicePackageGroupId { get; set; }
        /// <summary>
        /// Mã loại gói dịch vụ
        /// </summary>
        public int ServicePackageTypeId { get; set; }
        public virtual ServicePackageGroup ServicePackageGroup { get; } = null!;
        public virtual ServicePackageType ServicePackageType { get; } = null!;
        public virtual ICollection<EmployerOrderDetail> EmployerOrderDetails { get; } = new List<EmployerOrderDetail>();
        public virtual ICollection<EmployerServicePackageEmployerBenefit> EmployerServicePackageEmployerBenefits { get; } = new List<EmployerServicePackageEmployerBenefit>();
        public virtual ICollection<ServicePackageBenefit> ServicePackageBenefits { get; } = new List<ServicePackageBenefit>();
        public virtual ICollection<JobServicePackage> JobServicePackages { get; } = new List<JobServicePackage>();
        public virtual ICollection<EmployerCart> EmployerCarts { get; } = new List<EmployerCart>();
    }
}
