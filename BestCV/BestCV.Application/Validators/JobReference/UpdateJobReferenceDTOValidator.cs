using FluentValidation;
using BestCV.Application.Models.JobReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.JobReference
{
    public class UpdateJobReferenceDTOValidator : AbstractValidator<UpdateJobReferenceDTO>
    {
        public UpdateJobReferenceDTOValidator() {
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự");
        }
    }
}
