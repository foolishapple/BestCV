using BestCV.Application.Models.TopJobExtra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.TopCompany
{
    public class ChangeTopCompanyDTO
    {
        public TopCompanyDTO SlideUp { get; set; }
        public TopCompanyDTO SlideDown { get; set; }
    }
}
