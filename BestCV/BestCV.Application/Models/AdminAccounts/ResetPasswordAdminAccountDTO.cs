using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccounts
{
    public class ResetPasswordAdminAccountDTO
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool KeyUpToDate { get; set; } = true;
        public string Hash { get; set; }
    }
}
