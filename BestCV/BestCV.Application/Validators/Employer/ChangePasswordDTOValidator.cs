using FluentValidation;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Employer
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordEmployerDTO>
    {
        public ChangePasswordDTOValidator() 
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Mật khẩu cũ không được để trống. ");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Mật khẩu mới không được để trống. ");
            RuleFor(x => x.NewPassword).NotEqual(x => x.OldPassword).WithMessage("Mật khẩu mới không được giống mật khẩu cũ.");
            RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("Mật khẩu mới phải có từ 8 ký tự.");
            RuleFor(x => x.NewPassword).Matches("^(?=.*[A-Z]).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ viết hoa.");
            RuleFor(x => x.NewPassword).Matches("^(?=.*[a-z]).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ viết thường.");
            RuleFor(x => x.NewPassword).Matches("^(?=.*\\d).+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ số.");
            RuleFor(x => x.NewPassword).Matches("^(?=.*[\\W_]).+$").WithMessage("Mật khẩu mới phải có ít nhất một kỹ tự đặc biệt.");
            RuleFor(x => x.NewPasswordRepeat).NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống. ");
            RuleFor(x => x.NewPasswordRepeat).Equal(x => x.NewPassword).WithMessage("Mật khẩu mới và mật khẩu xác nhận không trùng khớp nhau.");
        }
    }
}
