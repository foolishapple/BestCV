using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.OrderStatus
{
    public class UpdateOrderStatusDTO : InsertOrderStatusDTO
    {
        public int Id { get; set; }
    }
}
