using FluentValidation;
using BestCV.Application.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators
{
    public class InsertJobDTOValidator : AbstractValidator<InsertJobDTO>
    {
        public InsertJobDTOValidator()
        {
            RuleFor(x => x.RecruimentCampaignId)
                .NotNull();
            RuleFor(x => x.JobStatusId)
                .NotNull();
            RuleFor(x => x.PrimaryJobCategoryId)
                .NotNull()
                .WithMessage("Ngành nghề chính không được để trống");
            RuleFor(x => x.TotalRecruitment)
                .NotEmpty()
                .NotNull()
                .WithMessage("Số lượng tuyển không được để trống");
            RuleFor(x => x.GenderRequirement)
                .NotNull()
                .WithMessage("Giới tính không được để trống");
            RuleFor(x => x.JobTypeId)
                .NotNull()
                .WithMessage("Vị trí công việc không được để trống");
            RuleFor(x => x.JobPositionId)
                .NotNull()
                .WithMessage("Ngành nghề chính không được để trống");
            RuleFor(x => x.ExperienceRangeId)
                .NotNull()
                .WithMessage("Kinh nghiệm không được để trống");
            RuleFor(x => x.Currency)
                .NotNull()
                .WithMessage("Loại tiền tệ không được để trống");
            RuleFor(x => x.SalaryTypeId)
                .NotNull()
                .WithMessage("Loại lương không được để trống");
            RuleFor(x => x.SalaryFrom)
                .GreaterThan(0)
                .WithMessage("Mức lương phải lớn hơn 0");
            RuleFor(x => x.SalaryTo)
                .GreaterThan(x => x.SalaryFrom)
                .WithMessage("Mức lương đến phải lớn hơn mức lương từ");
            RuleFor(x => x.Requirement)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yêu cầu công việc không được để trống");
            RuleFor(x => x.Benefit)
                .NotNull()
                .NotEmpty()
                .WithMessage("Quyền lợi ứng viên không được để trống");
            //RuleFor(x => x.ReceiverPhone)
            //    .Matches(@"([\\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})")
            //    .When(c => !string.IsNullOrEmpty(c.ReceiverPhone))
            //    .WithMessage("Số điện thoại chỉ gồm các chữ số & phải đúng số điện thoại Việt Nam");
            //RuleFor(x => x.ReceiverEmail)
            //    .EmailAddress()
            //    .When(c => !string.IsNullOrEmpty(c.ReceiverEmail))
            //    .WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tên tin tuyển dụng không được để trống");
        }
    }
}
