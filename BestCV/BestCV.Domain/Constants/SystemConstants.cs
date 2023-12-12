using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public static class SystemConstants
    {
        public static string SYSTEM_API_URL = "https://localhost:7059/";
        public static string CANDIDATE_API_URL = "http://localhost:7584/";
        public static string EMPLOYER_API_URL = "https://localhost:7042/";
        

        public static string ACCOUNT_META_VERIFY_EMAIL = "VERIFY_EMAIL";
        public static int POST_LAYOUT_ID_DEFAULT = 1001;
        public static int AUTHOR_ADMIN_ID = 1001;
    }
}
