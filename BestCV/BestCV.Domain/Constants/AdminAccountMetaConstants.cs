using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Constants
{
    public class AdminAccountMetaConstants
    {
        #region ForgotPassword

        public static string FORGOT_PASSWORD_EMAIL_KEY = "FORGOT_PASSWORD_EMAIL";
        public static int FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT = 1;
        public static int FORGOT_PASSWORD_SENT_EMAIL_LIMIT = 5;
        public static int FORGOT_PASSWORD_TOKEN_EXPRIED = 1;
        #endregion
    }
}
