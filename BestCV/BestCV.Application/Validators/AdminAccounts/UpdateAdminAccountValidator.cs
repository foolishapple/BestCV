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
    public class UpdateAdminAccountValidator : AbstractValidator<UpdateAdminAccountDTO>
    {
        public UpdateAdminAccountValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Email phải đúng định dạng.")
                .MaximumLength(500).WithMessage("Email có độ dài tối đa là 500 ký tự.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ và tên không được để trống.")
                .Matches(RegexConst.Format.FULL_NAME).WithMessage("Tên phải đúng định dạng")
                .MaximumLength(255).WithMessage("Họ và tên có độ dài tối đa là 255 ký tự.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên dăng nhập không được để trống.")
                .MaximumLength(255).WithMessage("Tên dăng nhập có độ dài tối đa là 255 ký tự.")
                .Matches(RegexConst.Format.USER_NAME).WithMessage("Tên đăng nhập phải đúng dịnh dạng.");
            RuleFor(x => x.Password).MaximumLength(500).WithMessage("Mật khẩu có độ dài tối đa là 500 ký tự.")
                .Matches(RegexConst.Format.PASSWORD).When(c=>!string.IsNullOrEmpty(c.Password)).WithMessage("Mật khẩu có độ dài tối thiểu là 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và 1 ký tự đặc biệt.");
            RuleFor(c => c.Phone).NotEmpty().WithMessage("Số điện thoại không được để trống")
                .MaximumLength(50).WithMessage("Số điện thoại có độ dài tối đa là 50 ký tự")
                .Matches(RegexConst.Format.PHONE).WithMessage("Số điện thoại phải đúng định dạng");
            RuleFor(x => x.Photo).MaximumLength(500).WithMessage("Đường dẫn ảnh có độ dài tối đa là 500 ký tự.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
            RuleFor(c => c.LockEnabled).NotNull().WithMessage("Bị khóa tài khoản không được để trống.");
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Mã tài khoản quản trị viên không chính xác");
        }
    }
}
