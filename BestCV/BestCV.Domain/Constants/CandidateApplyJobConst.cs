using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Constants
{
    public static class CandidateApplyJobConst
    {
    //    /// <summary>
    //    /// Trạng thái ứng viên ứng tuyển
    //    /// </summary>
    //    public enum Status
    //    {
    //        /// <summary>
    //        /// Tiếp nhận
    //        /// </summary>
    //        Receive = 1001,
    //        /// <summary>
    //        /// Phù hợp
    //        /// </summary>
    //        Fit = 1002,
    //        /// <summary>
    //        /// Hẹn phỏng vấn
    //        /// </summary>
    //        Interview = 1003,
    //        /// <summary>
    //        /// Gửi đề nghị
    //        /// </summary>
    //        Offer = 1004,
    //        /// <summary>
    //        /// Nhận việc
    //        /// </summary>
    //        GetAJob = 1005,
    //        /// <summary>
    //        /// Từ chối
    //        /// </summary>
    //        Reject = 1006,

    //    }

    //    public enum Source
    //    {
    //        /// <summary>
    //        /// Tự ứng tuyển
    //        /// </summary>
    //        Apply = 1001,
    //        /// <summary>
    //        /// Tìm CV
    //        /// </summary>
    //        FindCV = 1002,

    //    }

    }

    public static class CandidateApplyStatusId
    {
        /// <summary>
        /// Tiếp nhận
        /// </summary>
        public const int Receive = 1001;
        /// <summary>
        /// Phù hợp
        /// </summary>
        public const int Fit = 1002;
        /// <summary>
        /// Hẹn phỏng vấn
        /// </summary>
        public const int Interview = 1003;
        /// <summary>
        /// Gửi đề nghị
        /// </summary>
        public const int Offer = 1004;
        /// <summary>
        /// Nhận việc
        /// </summary>
        public const int GetAJob = 1005;
        /// <summary>
        /// Từ chối
        /// </summary>
        public const int Reject = 1006;
    }

    public static class CandidateApplySourceId
    {
        /// <summary>
        /// Tự ứng tuyển
        /// </summary>
        public const int Apply = 1001;
        /// <summary>
        /// Tìm CV
        /// </summary>
        public const int FindCV = 1002;
    }
}
