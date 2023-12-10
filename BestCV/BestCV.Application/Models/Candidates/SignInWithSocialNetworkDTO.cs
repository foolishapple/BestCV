using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class SignInWithSocialNetworkDTO
    {
        public string Id { get;set; }
        public string FullName { get;set; }
        public string Email { get;set; }
        public string Photo { get; set ; } = "";
        public string Phone { get; set; } = "";
    }
}
