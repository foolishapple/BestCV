using FluentValidation;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.AdminAccounts
{
    public class ChangePasswordAdminAccountValidator : AbstractValidator<ChangePasswordAdminAccountDTO>
    {
        public ChangePasswordAdminAccountValidator()
        {
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage("Mật khẩu mới không được để trống.")
                .MaximumLength(500).WithMessage("Mật khẩu mới có độ dài tối đa là 500 ký tự.")
                .Matches(RegexConst.Format.PASSWORD).WithMessage("Mật khẩu mới có độ dài tối thiểu là 8 ký tự, 1 chữ cái viết hoa, 1 chữ cái viết thướng và 1 ký tự đặc biệt.");
            RuleFor(c => c.ReNewPassword).NotEmpty().WithMessage("Nhập lại mật khẩu mới không được để trống.")
                .MaximumLength(500).WithMessage("Nhập lại mật khẩu mới có độ dài tối đa là 500 ký tự.")
                .Matches(RegexConst.Format.PASSWORD).WithMessage("Nhập lại mật khẩu mới có độ dài tối thiểu là 8 ký tự, 1 chữ cái viết hoa, 1 chữ cái viết thướng và 1 ký tự đặc biệt.")
                .When(c => c.ReNewPassword == c.NewPassword).WithMessage("Nhập lại mật khẩu mới phải giống với mật khẩu mới.");
        }
    }
}
