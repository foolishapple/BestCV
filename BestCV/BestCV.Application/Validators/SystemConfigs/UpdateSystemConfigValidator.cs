using FluentValidation;
using BestCV.Application.Models.SystemConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.SystemConfigs
{
    public class UpdateSystemConfigValidator : AbstractValidator<UpdateSystemConfigDTO>
    {
        public UpdateSystemConfigValidator()
        {
            RuleFor(c => c.Key).NotEmpty().WithMessage("Từ khóa không được để trống")
                .MaximumLength(255).WithMessage("Từ khóa có dộ dài tối đa là 255 ký tự.");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Giá trị không được để trống.")
                .MaximumLength(255).WithMessage("Giá trị có độ dài tối đa là 255 ký tự");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
