using FluentValidation;
using BestCV.Application.Models.Candidates;
using BestCV.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Candidate
{
    public class ChangePasswordCandidateValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordCandidateValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Mật khẩu cũ không được để trống");
            
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Mật khẩu mới không được để trống");
            RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("Mật khẩu mới phải có từ 8 ký tự");
            RuleFor(x => x.NewPassword)
                .Matches("[A-Z]").WithMessage("Mật khẩu cần ít nhất 1 ký tự in hoa.")
                .Matches("[a-z]").WithMessage("Mật khẩu cần ít nhất 1 ký tự thường.")
                .Matches(@"\d").WithMessage("Mật khẩu cần ít nhất 1 số.")
                .Matches(@"[][""!#@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Mật khẩu cần ít nhất 1 ký tự đặc biệt.")
                .Matches("^[^£ “”]*$").WithMessage("Mật khẩu không được chứa các ký tự £ “” hoặc khoảng trắng.");
    

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword).WithMessage("Mật khẩu mới và mật khẩu xác nhận không trùng khớp nhau");
        }
    }
}
