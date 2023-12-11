using FluentValidation;
using BestCV.Application.Models.EmployerActivityLogTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerActivityLogTypes
{
    public class InsertEmployerActivityLogTypeValidator : AbstractValidator<InsertEmployerActivityLogTypeDTO>
    {
        public InsertEmployerActivityLogTypeValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên có độ dài tôi đa là 255 ký tự.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
