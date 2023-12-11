using FluentValidation;
using BestCV.Application.Models.TopJobUrgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.TopJobUrgent
{
    public class InsertTopJobUrgentDTOValidator : AbstractValidator<InsertTopJobUrgentDTO>
    {
        public InsertTopJobUrgentDTOValidator()
        {
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
