using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.License
{
    public class InsertLicenseDTO
    {
        /// <summary>
        /// Mã công ty
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Mã loại giấy phép
        /// </summary>
        public int LicenseTypeId { get; set; }

        /// <summary>
        /// Ảnh giấy phép
        /// </summary>
        public string Path { get; set; }
    }
}
