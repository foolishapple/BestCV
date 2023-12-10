using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class EmployerOrderDetailDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int EmployerServicePackageId { get; set; }
        public string EmployerServicePackageName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int FinalPrice { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
