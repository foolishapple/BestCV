using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.InterviewSchdule
{
    public class InsertInterviewScheduleDTO
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Loại lịch phỏng vấn
        /// </summary>
        public int InterviewscheduleTypeId { get; set; }
        /// <summary>
        /// Trạng thái lịch phỏng vấn
        /// </summary>
        public int InterviewscheduleStatusId { get; set; }
        /// <summary>
        /// Mã ứng viên nôp hồ sơ việc làm
        /// </summary>
        public long CandidateApplyJobId { get; set; }
        /// <summary>
        /// Đường link
        /// </summary>
        public string Link { get; set; }
        public DateTime StateDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Vị trí
        /// </summary>
        public string Location { get; set; }
        public string Search { get; set; }
    }
}
