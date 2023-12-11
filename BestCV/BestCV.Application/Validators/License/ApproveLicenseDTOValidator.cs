using FluentValidation;
using BestCV.Application.Models.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.License
{
    public class ApproveLicenseDTOValidator : AbstractValidator<ApproveLicenseDTO>
    {
        public ApproveLicenseDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Mã giấy tờ không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã giấy tờ phải lớn hơn 0.");
        }
    }
}
