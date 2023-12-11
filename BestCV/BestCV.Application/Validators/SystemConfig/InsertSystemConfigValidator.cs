using FluentValidation;
using BestCV.Application.Models.SystemConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.SystemConfig
{
    public class InsertSystemConfigValidator : AbstractValidator<InsertSystemConfigDTO>
    {
        public InsertSystemConfigValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Giá trị không được để trống.")
                .MaximumLength(255).WithMessage("Giá trị không được vượt quá 255 ký tự");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
