using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.TopCompany
{
    public class TopCompanyDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
