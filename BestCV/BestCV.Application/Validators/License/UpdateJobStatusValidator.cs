using FluentValidation;
using BestCV.Application.Models.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Licensees
{
    public class UpdateLicenseValidator : AbstractValidator<UpdateLicenseDTO>
    {
        public UpdateLicenseValidator()
        {
            RuleFor(x => x.Path).NotNull().WithMessage("Đường dẫn giấy tờ không được để trống ");
            RuleFor(x => x.IsApproved).Must(IsApproved => IsApproved == true || IsApproved == false).WithMessage("Xác nhận giấy tờ phải là có hoặc không");
        }
    }
}
