using FluentValidation;
using BestCV.Application.Models.JobServicePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.JobServicePackages
{
    public class UpdateJobServicePackageDTOValidator : AbstractValidator<UpdateJobServicePackageDTO>
    {
        public UpdateJobServicePackageDTOValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã không được để trống.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
            RuleFor(c => c.JobId).NotEmpty().WithMessage("Mã tin tuyển dụng không được để trống.");
            RuleFor(c => c.EmployerServicePackageId).NotEmpty().WithMessage("Mã dich vụ không được để trống");
        }
    }
}
