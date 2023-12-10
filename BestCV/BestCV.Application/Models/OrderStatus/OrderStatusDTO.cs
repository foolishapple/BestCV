using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.OrderStatus
{
    public class OrderStatusDTO : EntityCommon<int>
    {
        public string Color { get; set; }
    }
}
