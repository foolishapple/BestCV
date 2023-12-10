using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.VoucherTypes
{
    public class UpdateVoucherTypeDTO : InsertVoucherTypeDTO
    {
        /// <summary>
        /// Mã loại phiếu giảm giá
        /// </summary>
        public int Id { get; set; }
    }
}
