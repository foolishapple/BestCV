using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.License
{
    public class GetListLicenseByCompanyIdDTO : InsertLicenseDTO
    {
        public long Id { get; set; }
        public string LicenseTypeName { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
