using FluentValidation;
using BestCV.Application.Models.InterviewSchdule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.InterviewSchedule
{
    public class UpdateInterviewScheduleDTOValidator :AbstractValidator<UpdateInterviewScheduleDTO>
    {
        public UpdateInterviewScheduleDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Mã phỏng vấn không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã phỏng vấn phải lớn hơn 0.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.");
            RuleFor(x => x.InterviewscheduleTypeId)
                .NotEmpty().WithMessage("Loại phỏng vấn không được để trống.");
            RuleFor(x => x.InterviewscheduleStatusId)
                .NotEmpty().WithMessage("Trạng thái phỏng vấn không được để trống.");
            RuleFor(x => x.CandidateApplyJobId)
                .NotEmpty().WithMessage("Mã ứng tuyển phỏng vấn không được để trống.");
            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("Link phỏng vấn không được để trống.")
                .MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự.");
            RuleFor(x => x.StateDate)
                .NotEmpty().WithMessage("Ngày bắt đầu phỏng vấn không được để trống.");
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc phỏng vấn không được để trống.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Địa điểm phỏng vấn không được để trống.");
            RuleFor(x => x.Search)
                .NotEmpty().WithMessage("Tìm kiếm phỏng vấn không được để trống.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự.");
        }
    }
}
