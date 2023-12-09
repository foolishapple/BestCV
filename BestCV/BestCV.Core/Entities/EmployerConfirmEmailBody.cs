using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class EmployerConfirmEmailBody
    {
        public string Fullname { get; set; }

        public string Otp { get; set; }

        public string ActiveLink { get; set; }

        public int Time { set; get; }
    }
}
