using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.JobServicePackages
{
    public class UpdateJobServicePackageDTO : InsertJobServicePackageDTO
    {
        public long Id { get; set; }
    }
}
