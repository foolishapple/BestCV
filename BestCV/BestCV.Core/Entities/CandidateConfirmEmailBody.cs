using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class CandidateConfirmEmailBody
    {
        public string Fullname { get; set; }
        public string Otp { get; set; }
        public string ActiveLink { get; set; }
        public int Time { get; set; }
    }
}
