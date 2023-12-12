using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public static class NotificationConfig
    {
        public const int FadeTimne = 30000;
        /// <summary>
        /// Maximum notification show in header homepage
        /// </summary>
        public const int MaxShowNumber = 5;
        public static class Status
        {
            public const int Read = 1001;
            public const int Unread = 1002;
        }
        public static class Type
        {
            public const int Candidate = 1001;
            public const int Employer = 1002;
            public const int Admin = 1003;
        }
    }
}
