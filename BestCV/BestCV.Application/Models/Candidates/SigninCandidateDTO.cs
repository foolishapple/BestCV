using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class SigninCandidateDTO
    {
        /// <summary>
        /// Email hoặc số điện thoại
        /// </summary>
        public string EmailorPhone { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
        
    }
}
