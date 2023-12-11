using FluentValidation;
using BestCV.Application.Models.TopJobExtra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.TopJobExtra
{
    public class InsertTopJobExtraDTOValidator : AbstractValidator<InsertTopJobExtraDTO>
    {
        public InsertTopJobExtraDTOValidator()
        {
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
