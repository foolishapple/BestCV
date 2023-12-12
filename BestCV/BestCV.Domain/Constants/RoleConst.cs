using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public static class RoleConst
    {
        /// <summary>
        /// Mã quyền nhà tuyển dụng
        /// </summary>
        public const int EMPlOYER_ID = 1006;
        /// <summary>
        /// Mã quyền ứng viên
        /// </summary>
        public const int CANDIDATE_ID = 1007;
        /// <summary>
        /// Những quyền đặc biệt
        /// </summary>
        public static int[] SPECIAL_ROLE = { EMPlOYER_ID, CANDIDATE_ID };
    }
}
