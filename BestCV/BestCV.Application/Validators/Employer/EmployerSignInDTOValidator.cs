using FluentValidation;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Employer
{
    public class EmployerSignInDTOValidator : AbstractValidator<SignInEmployerDTO>
    {
        public EmployerSignInDTOValidator()
        {
            RuleFor(e => e.EmailOrPhone)
                .NotEmpty()
                .WithMessage("Email hoặc số điện thoại không được để trống.");
            RuleFor(e => e.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống.");
        }
    }
}
