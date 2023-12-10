using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.License
{
    public class UpdateLicenseDTO : InsertLicenseDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// Chấp thuận
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Ngày chấp thuận
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
    }
}
