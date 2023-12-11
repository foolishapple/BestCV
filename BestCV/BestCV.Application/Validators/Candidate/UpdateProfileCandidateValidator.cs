using FluentValidation;
using BestCV.Application.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Candidate
{
    public class UpdateProfileCandidateValidator : AbstractValidator<UpdateProfileCandidateDTO>
    {
        public UpdateProfileCandidateValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ và tên không được để trống");
            RuleFor(x => x.FullName).MaximumLength(255).WithMessage("Họ và tên có độ dài tối đa là 255 ký tự");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email không được để trống.");
            RuleFor(x => x.Email).MaximumLength(255).WithMessage("Email có độ dài tối đa là 255 ký tự");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Định dạng email không hợp lệ.");

            RuleFor(x => x.Phone).NotNull().WithMessage("Số điện thoại không được để trống.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Số điện thoại không được để trống.");
            RuleFor(x => x.Phone).MaximumLength(10).WithMessage("Số điện thoại có độ dài tối đa là 10 ký tự.");
            RuleFor(x => x.Phone).Matches("(84|0[3|5|7|8|9])+([0-9]{8})").WithMessage("Số điện thoại chỉ được chứa các ký tự số.");

            RuleFor(x => x.AddressDetail).MaximumLength(255).WithMessage("Địa chỉ có độ dài tối đa là 255 ký tự");
            RuleFor(x => x.JobPosition).MaximumLength(255).WithMessage("Nghề nghiệp có độ dài tối đa là 255 ký tự");
            RuleFor(x => x.Interests).MaximumLength(255).WithMessage("Sở thích có độ dài tối đa là 255 ký tự");
            RuleFor(x => x.Objective).MaximumLength(500).WithMessage("Mục tiêu có độ dài tối đa là 500 ký tự");

            RuleFor(x => x.Photo).MaximumLength(500).WithMessage("Ảnh đại diện không đúng kích cỡ");
            RuleFor(x => x.CoverPhoto).MaximumLength(500).WithMessage("Ảnh bìa không đúng kích cỡ");

            
            
    }
    }
}
