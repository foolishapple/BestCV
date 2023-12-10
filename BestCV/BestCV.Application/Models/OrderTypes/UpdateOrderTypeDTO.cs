using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.OrderTypes
{
    public class UpdateOrderTypeDTO : InsertOrderTypeDTO
    {
        /// <summary>
        /// Mã loại hóa đơn
        /// </summary>
        public int Id { get; set; }
    }
}
