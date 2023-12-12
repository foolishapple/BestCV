using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public class EmployerMetaConstants
    {
        public static string CONFIRM_EMAIL_KEY = "CONFIRM_EMAIL";

        public static int CONFIRM_EMAIL_EXPIRY_TIME = 1;

        public static int MAXIMUM_SEND_CONFIRM_EMAIL_PER_DAY = 5;

        #region ForgotPassword

        public static string FORGOT_PASSWORD_EMAIL_KEY = "FORGOT_PASSWORD_EMAIL";
        public static int FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT = 1;
        public static int FORGOT_PASSWORD_SENT_EMAIL_LIMIT = 5;
        public static int FORGOT_PASSWORD_TOKEN_EXPRIED = 1;
        #endregion
    }
}
