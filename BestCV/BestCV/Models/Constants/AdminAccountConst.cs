using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public class AdminAccountConst
    {
        public const string DEFAULT_PHOTO = "/uploads/admin/default_avatar.jpg";
        /// <summary>
        /// super admin id
        /// </summary>
        public const long SUPER_ADMIN_ID = 1001;
        public const long ADMIN_ID = 1002;
        public static long[] ADMIN_ROLE = new long[] { SUPER_ADMIN_ID, ADMIN_ID };
    }
}
