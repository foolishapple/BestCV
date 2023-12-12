using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Constants
{
    public class EmployerServicePackageConst
    {
        /// <summary>
        /// 1 lần trong 32 ngày
        /// </summary>
        public const int Refresh_Job = 1023;
        /// <summary>
        /// 3 lần trong 32 ngày. Ngày thứ 7, 14 và 21
        /// </summary>
        public const int Auto_Refresh_Job_Weekly = 1024;
        /// <summary>
        /// 9 lần trong 32 ngày. 9 lần trong 32 ngày
        /// </summary>
        public const int Auto_Refresh_Job_Flexible = 1025;
        /// <summary>
        /// 8 ngày liên tiếp vào thời gian lựa chọn.Tuần 1, Tuần 2, Tuần 3, Tuần 4
        /// </summary>
        public const int Auto_Refresh_Job_Daily_8_days = 1026;
        /// <summary>
        /// 32 ngày liên tiếp
        /// </summary>
        public const int Auto_Refresh_Job_Daily_32_days = 1027;
    }
}
