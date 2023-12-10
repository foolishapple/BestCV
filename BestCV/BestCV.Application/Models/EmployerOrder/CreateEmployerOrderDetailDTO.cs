using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class CreateEmployerOrderDetailDTO
    {
        public int EmployerServicePackageId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int DiscountPrice { get; set; } = 0;
        public int FinalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
        public string Search { get; set; } = "";

    }
}
