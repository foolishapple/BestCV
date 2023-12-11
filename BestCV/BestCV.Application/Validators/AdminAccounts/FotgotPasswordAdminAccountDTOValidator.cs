using FluentValidation;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.AdminAccounts
{
    public class FotgotPasswordAdminAccountDTOValidator : AbstractValidator<FotgotPasswordAdminAccountDTO>
    {
        public FotgotPasswordAdminAccountDTOValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống. ");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email không đúng định dạng.");
            RuleFor(x => x.Email).MaximumLength(255).WithMessage("Email không được vượt quá 255 kí tự.");
        }
    }
}
