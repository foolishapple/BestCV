using FluentValidation;
using Hangfire.Annotations;
using BestCV.Application.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Candidate
{
    public class SignupCandidateValidator : AbstractValidator<SignupCandidateDTO>
    {
        public SignupCandidateValidator() {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Tên đầy đủ không được để trống.");
            RuleFor(x => x.FullName).MaximumLength(255).WithMessage("Tên đầy đủ có độ dài tối đa là 255 ký tự.");

            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email không được để trống.");
            RuleFor(x => x.Email).MaximumLength(255).WithMessage("Email đăng nhập có độ dài tối đa là 255 ký tự");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Định dạng email không hợp lệ.");

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Mật khẩu không được để trống.");
            RuleFor(x => x.Password).Length(8,255).WithMessage("Mật khẩu có độ dài từ 8 đến 255 ký tự");
            RuleFor(x => x.Password).Matches(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]+$").WithMessage("Mật khẩu mới phải có ít nhất một chữ viết hoa và một ký tự đặc biệt");

            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Xác nhận mật khẩu không được để trống.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Mật khẩu và mật khẩu xác nhận không khớp.");

            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Số điện thoại không được để trống.");
            RuleFor(x => x.Phone).MaximumLength(10).WithMessage("Số điện thoại có độ dài tối đa là 10 ký tự.");
            RuleFor(x => x.Phone).Matches("(84|0[3|5|7|8|9])+([0-9]{8})").WithMessage("Số điện thoại chỉ được chứa các ký tự số.");
        }
    }
}
