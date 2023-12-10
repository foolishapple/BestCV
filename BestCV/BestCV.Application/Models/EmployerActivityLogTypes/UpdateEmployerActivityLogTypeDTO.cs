using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerActivityLogTypes
{
    public class UpdateEmployerActivityLogTypeDTO : InsertEmployerActivityLogTypeDTO
    {
        /// <summary>
        /// Mã loại lịch sử hoạt động của nhà tuyển dụng
        /// </summary>
        public int Id { get; set; }
    }
}
