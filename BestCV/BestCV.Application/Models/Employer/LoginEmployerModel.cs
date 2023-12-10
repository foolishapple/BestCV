using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Employer
{
    public class LoginEmployerModel
    {
        public string Fullname { get; set; } = null!;

        public string? Photo { get; set; }

        public string? Token { get; set; }

        //public string Username { get; set; } = null!;

        //public string Email { get; set; } = null!;
    }
}
