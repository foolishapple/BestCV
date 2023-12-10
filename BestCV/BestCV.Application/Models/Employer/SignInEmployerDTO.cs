using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Employer
{
    public class SignInEmployerDTO
    {
        public string EmailOrPhone { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
