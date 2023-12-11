using FluentValidation;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.AdminAccounts
{
    public class SignInAdminAccountValidator : AbstractValidator<SignInAdminAccountDTO>
    {
        public SignInAdminAccountValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .Must(c => ValidateUserName(c)).WithMessage("Tên đăng nhập phải đúng định dạng")
                .MaximumLength(255).WithMessage("Tên đăng nhập có độ dài tối đa là 255 ký tự.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống.")
                .MaximumLength(500).WithMessage("Mật khẩu có độ dài tối đa là 500 ký tự.")
                .Matches(RegexConst.Format.PASSWORD).WithMessage("Mật khẩu có độ dài tối thiểu là 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường và một số");
        }
        public bool ValidateUserName(string userName)
        {
            if (userName.Contains("@")){
                return userName.IsMatch(RegexConst.Format.EMAIL);
            }
            else
            {
                return userName.IsMatch(RegexConst.Format.USER_NAME);
            }
        }
    }
}
