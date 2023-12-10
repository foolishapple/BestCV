using BestCV.Application.Models.ServicePackageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.ServicePackageGroup
{
    public class UpdateServicePackageGroupDTO : InsertServicePackageTypeDTO
    {
        public int Id { get; set; }
    }
}
