using FluentValidation;
using BestCV.Application.Models.Permissions;
using BestCV.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Permissions
{
    public class InsertPermissionValidator : AbstractValidator<InsertPermissionDTO>
    {
        public InsertPermissionValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(255).WithMessage("Tên có độ dài tối đa là 255 ký tự")
                .Matches(RegexConst.Format.FULL_NAME).WithMessage("Tên phải đúng định dạng.");
            RuleFor(c => c.Code).NotEmpty().WithMessage("Mã CODE không được để trống")
                .MaximumLength(255).WithMessage("Mã CODE có độ dài tối đa là 255 ký tự");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
