using FluentValidation;
using BestCV.Application.Models.CandidateSaveJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateSaveJob
{
    public class UpdateCandidateSaveJobDTOValidator : AbstractValidator<UpdateCandidateSaveJobDTO>
    {
        public UpdateCandidateSaveJobDTOValidator()
        {
            RuleFor(c => c.CandidateId).NotEmpty().WithMessage("Mã ứng viên không được để trống.");
            RuleFor(c => c.JobId).NotEmpty().WithMessage("Mã công việc không được để trống.");
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã không được để trống.");
        }
    }
}
