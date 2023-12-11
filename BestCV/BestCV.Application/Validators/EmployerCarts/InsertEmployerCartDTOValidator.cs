using FluentValidation;
using BestCV.Application.Models.EmployerCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerCarts
{
    public class InsertEmployerCartDTOValidator : AbstractValidator<InsertEmployerCartDTO>
    {
        public InsertEmployerCartDTOValidator()
        {
            RuleFor(c => c.EmployerServicePackageId).NotEmpty().WithMessage("Mã dịch vụ không được để trống.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
