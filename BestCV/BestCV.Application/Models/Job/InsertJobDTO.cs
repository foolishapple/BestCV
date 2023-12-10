using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Job
{
    public class InsertJobDTO
    {
        /// <summary>
        /// Mã chiến dịch tuyển dụng
        /// </summary>
        public long RecruimentCampaignId { get; set; }

        /// <summary>
        /// Mã trạng thái tin tuyển dụng
        /// </summary>
        public int JobStatusId { get; set; }

        /// <summary>
        /// Mã cấp bậc của tin tuyển dụng
        /// </summary>
        public int PrimaryJobCategoryId { get; set; }

        /// <summary>
        /// Số lượng tuyển
        /// </summary>
        public int TotalRecruitment { get; set; }

        /// <summary>
        /// Giới tính yêu cầu
        /// </summary>
        public int GenderRequirement { get; set; }

        /// <summary>
        /// Mã loại tin tuyển dụng
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// Mã ngành nghề của tin tuyển dụng
        /// </summary>
        public int JobPositionId { get; set; }


        /// <summary>
        /// Khoảng kinh nghiệm yêu cầu
        /// </summary>
        public int ExperienceRangeId { get; set; }

        /// <summary>
        /// Loại tiền tệ
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Mã loại lương
        /// </summary>
        public int SalaryTypeId { get; set; }

        /// <summary>
        /// Lương tối thiểu
        /// </summary>
        public int? SalaryFrom { get; set; }

        /// <summary>
        /// Lương tối đa
        /// </summary>
        public int? SalaryTo { get; set; }

        /// <summary>
        /// Tổng quan công việc
        /// </summary>
        public string? Overview { get; set; }

        /// <summary>
        /// Yêu cầu công việc
        /// </summary>
        public string Requirement { get; set; } = null!;

        /// <summary>
        /// Lợi ích
        /// </summary>
        public string Benefit { get; set; } = null!;

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string? ReceiverName { get; set; }

        /// <summary>
        /// Điện thoại người nhận
        /// </summary>
        public string? ReceiverPhone { get; set; }

        /// <summary>
        /// Email người nhận
        /// </summary>
        public string? ReceiverEmail { get; set; }

        /// <summary>
        /// Thời hạn ứng tuyển
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }


        public DateTime CreatedTime { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }


        /// <summary>
        /// Ngành nghề phụ
        /// </summary>
        public List<int> ListJobSecondaryJobCategory { get; set; } = new();

        /// <summary>
        /// lý do ứng tuyển
        /// </summary>
        public List<string> ListJobReasonApply { get; set; } = new();

        /// <summary>
        /// Nơi làm việc
        /// </summary>
        public List<JobWorkPlaceDTO> ListJobRequireWorkplace { get; set; } = new();


        /// <summary>
        /// Kỹ năng yêu cầu
        /// </summary>
        public List<int> ListJobRequireSkill { get; set; } = new();

        /// <summary>
        /// Mã thẻ của tin tuyển dụng
        /// </summary>
        public List<int> ListTag { get; set; } = new();

    }
}
