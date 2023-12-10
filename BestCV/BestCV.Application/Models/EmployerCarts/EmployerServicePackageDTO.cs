using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerCarts
{
    public class EmployerCartDTO
    {
        public long Id { get; set; }
        public long EmployerId { get; set; }
        public int EmployerServicePackageId{ get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
