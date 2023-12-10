using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.WorkPlace
{
    public class DistrictDTO
    {
        public string name { get; set; } = null!;

        public int code { get; set; }

        public string division_type { get; set; } = null!;

        public string codename { get; set; } = null!;

        public int province_code { get; set; }
    }
}
