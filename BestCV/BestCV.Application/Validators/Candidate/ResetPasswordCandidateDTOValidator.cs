using FluentValidation;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Candidate
{
    public class ResetPasswordCandidateDTOValidator : AbstractValidator<ResetPasswordCandidateDTO>
    {
        public ResetPasswordCandidateDTOValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu mới không được để trống. ");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Mật khẩu mới phải có từ 8 ký tự.");
            RuleFor(x => x.Password).Matches("^(?=.*[A-Z]).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ viết hoa.");
            RuleFor(x => x.Password).Matches("^(?=.*[a-z]).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ viết thường.");
            RuleFor(x => x.Password).Matches("^(?=.*\\d).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ số.");
            RuleFor(x => x.Password).Matches("^(?=.*[\\W_]).+$").WithMessage("Mật khẩu mới phải có ít nhất một kỹ tự đặc biệt.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống. ");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Mật khẩu mới và mật khẩu xác nhận không trùng khớp nhau.");
        }
    }
}
