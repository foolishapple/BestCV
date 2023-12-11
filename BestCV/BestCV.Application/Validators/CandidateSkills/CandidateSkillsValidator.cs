using FluentValidation;
using BestCV.Application.Models.CandidateSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateSkills
{
    public class CandidateSkillsValidator : AbstractValidator<CandidateSkillDTO>
    {
        public CandidateSkillsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên kỹ năng không được để trống");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên kỹ năng có độ dài tối đa 255 ký tự");

        }
    }
}
