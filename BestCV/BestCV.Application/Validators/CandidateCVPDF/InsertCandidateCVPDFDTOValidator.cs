using FluentValidation;
using BestCV.Application.Models.CandidateCVPDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateCVPDF
{
    public class InsertCandidateCVPDFDTOValidator : AbstractValidator<InsertCandidateCVPDFDTO>
    {
        public InsertCandidateCVPDFDTOValidator()
        {
            RuleFor(c => c.CandidateId).NotEmpty().WithMessage("Mã ứng viên không được để trống.");
            RuleFor(c => c.CandidateCVPDFTypeId).NotEmpty().WithMessage("Mã loại CV PDF của ứng viên không được để trống.");
            RuleFor(c => c.Url).NotNull().WithMessage("Đường dẫn file không được để trống.");
        }
    }
}
