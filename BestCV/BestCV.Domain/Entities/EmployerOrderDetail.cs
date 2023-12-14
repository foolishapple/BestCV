using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class EmployerOrderDetail : EntityBase<long>, IFullTextSearch
    {
        public long OrderId { get; set; }
        /// <summary>
        /// Mã gói dịch vụ nhà tuyển dụng
        /// </summary>
        public int EmployerServicePackageId { get; set; }
        public int Quantity { get; set; }   
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int FinalPrice { get; set; }
        public string Search { get; set ; }

        public virtual EmployerOrder EmployerOrder { get; set; }

        public virtual EmployerServicePackage EmployerServicePackage { get; set; }
        public virtual ICollection<EmployerServicePackageEmployer> EmployerServicePackageEmployers { get; } = new List<EmployerServicePackageEmployer>();
    }
}
