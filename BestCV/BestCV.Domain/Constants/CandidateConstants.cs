using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Constants
{
    public class CandidateConstants
    {
        public const string DEFAULT_PHOTO = "/assets/images/avartar.jpg";
        public const string DEFAULT_CONVER_PHOTO = "/assets/images/avartar.jpg";
        public static string CANDIDATE_META_VERIFY_EMAIL = "VERIFY_EMAIL";

        public static int DEFAULT_VALUE_CANDIDATE_LEVEL = 1001;
        public static int DEFAULT_VALUE_CANDIDATE_STATUS = 1001;
        public static string DEFAULT_PASSWORD = "123456";

        public static int DEFAULT_VALUE_CANDIDATE_SUGGESTION_EXPRERIENCE_RANGE = 1001;
        public static int DEFAULT_VALUE_CANDIDATE_SUGGESTION_SALARY_RANGE = 1001;
        
        public static int PASSSWORD_SENT_EMAIL_TIME_LIMIT = 1;
        public static int PASSSWORD_SENT_EMAIL_LIMIT = 5;

        public static string ERROR_EMAIL_INVALID = "Email không hợp lệ.";
        public static string ERROR_WHEN_SIGN_IN_SNSID = "Có lỗi khi đăng nhập tài khoản, vui lòng thử lại.";
        public static string ERROR_NOT_ACTIVATED = "Tài khoản của bạn chưa được kích hoạt, vui lòng kiểm tra email để kích hoạt tài khoản.";

        #region ForgotPassword

        public static string FORGOT_PASSWORD_EMAIL_KEY = "FORGOT_PASSWORD_EMAIL";
        public static int FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT = 1;
        public static int FORGOT_PASSWORD_SENT_EMAIL_LIMIT = 5;
        public static int FORGOT_PASSWORD_TOKEN_EXPRIED = 1;
        #endregion
    }
}
