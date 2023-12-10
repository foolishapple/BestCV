using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.WorkPlace
{
    public class WorkplaceDTO : EntityCommon<int>
    {
        public int? ParentId { get; set; }
        public int Code { get; set; }

        public string DivisionType { get; set; } = null!;

        public string CodeName { get; set; } = null!;

        public int? PhoneCode { get; set; }

        public int? ProvinceCode { get; set; }
    }
}
