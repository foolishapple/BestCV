using FluentValidation;
using BestCV.Application.Models.TopJobManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.TopJobManagement
{
    public class InsertTopJobManagementValidator : AbstractValidator<InsertTopJobManagementDTO>
    {
        public InsertTopJobManagementValidator()
        {
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
