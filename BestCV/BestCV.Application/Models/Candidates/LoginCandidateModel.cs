using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class LoginCandidateModel
    {
        public string Fullname { get; set; } = null!;

        public string? Photo { get; set; }

        public string? Token { get; set; }
    }
}
