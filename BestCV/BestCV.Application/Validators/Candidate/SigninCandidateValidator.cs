using FluentValidation;
using BestCV.Application.Models.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Candidate
{
    public class SigninCandidate : AbstractValidator<SigninCandidateDTO>
    {
        public SigninCandidate()
        {
            RuleFor(e => e.EmailorPhone)
                .NotEmpty()
                .WithMessage("Email hoặc số điện thoại không được để trống.");
            RuleFor(e => e.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống.");
        }
    }
}
