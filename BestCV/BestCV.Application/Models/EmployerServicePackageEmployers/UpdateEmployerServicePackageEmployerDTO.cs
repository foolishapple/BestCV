using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerServicePackageEmployers
{
    public class UpdateEmployerServicePackageEmployerDTO : InsertEmployerServicePackageEmployerDTO
    {
        /// <summary>
        /// Mã dịch vụ của nhà tuyển dụng
        /// </summary>
        public long Id { get; set; }
    }
}
