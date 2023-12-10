using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class EmployerOrderAndOrderDetailDTO
    {
        public int DiscountVoucher { get; set; }
        public List<EmployerOrderDetailDTO> ListOrderDetail { get; set; }
    }
}
