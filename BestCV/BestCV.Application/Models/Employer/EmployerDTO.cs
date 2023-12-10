using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Employer
{
    public class EmployerDTO
    {
        public long Id { get; set; } = 0;
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string? Phone { get; set; }
        public int PositionId { get; set; }
        public string? Photo { get; set; }
        public string? SkypeAccount { get; set; }
    }
}
