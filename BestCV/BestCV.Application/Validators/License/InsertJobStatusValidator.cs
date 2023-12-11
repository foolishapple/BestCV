using FluentValidation;
using BestCV.Application.Models.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Licensees
{
    public class InsertLicenseValidator : AbstractValidator<InsertLicenseDTO>
    {
        public InsertLicenseValidator()
        {
            RuleFor(x => x.Path).NotNull().WithMessage("Đường dẫn giấy tờ không được để trống ");
        }
    }
}
