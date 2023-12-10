using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Employer
{
    public class EmployerSignUpDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? SkypeAccount { get; set; }
        public int Gender { get; set; }
        public int PositionId { get; set; }
    }
}
