using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class EmployerServicePackageEmployerBenefit : EntityBase<int>
    {
        /// <summary>
        /// Mã gói lợi ích nhà tuyển dụng
        /// </summary>
        public int EmployerServicePackageId { get; set; }

        /// <summary>
        /// Mã quyền lợi của nhà tuyển dụng
        /// </summary>
        public int EmployerBenefitId { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Có được hưởng quyền lợi không
        /// </summary>
        public bool HasBenefit { get; set; }

        public virtual EmployerServicePackage EmployerServicePackage { get; set; }
        public virtual EmployerBenefit EmployerBenefit { get; set; }
    }
}
