using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccounts
{
    public class ChangePasswordAdminAccountDTO
    {
        /// <summary>
        /// Ad
        /// </summary>
        public long Id { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ReNewPassword { get; set; } = null!;

    }
}
