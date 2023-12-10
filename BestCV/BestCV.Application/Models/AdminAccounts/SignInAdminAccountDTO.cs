using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccounts
{
    public class SignInAdminAccountDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;   
    }
}
