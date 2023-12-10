using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.AdminAccounts
{

    public class UpdateAdminAccountDTO : InsertAdminAccountDTO
    {
        /// <summary>
        /// Mã tài khoản admin
        /// </summary>
        public int Id { get; set; }
    }
}
