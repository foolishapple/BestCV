using FluentValidation;
using BestCV.Application.Models.Employer;
using BestCV.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Employer
{
    public class UpdateCompanyDTOValidator : AbstractValidator<UpdateEmployerDTO>
    {
        public UpdateCompanyDTOValidator()
        {
            RuleFor(x => x.Fullname)
                 .NotNull()
                 .WithMessage("Họ và tên không được để trống");
            RuleFor(x => x.Fullname)
                 .Matches(RegexConst.Format.FULL_NAME)
                 .WithMessage("Tên không được chứa ký tự đặc biệt.");
            RuleFor(x => x.Fullname)
                 .Length(0, 255)
                 .WithMessage("Họ và tên không được vượt quá 255 ký tự");
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Giới tính không được để trống");
            RuleFor(x => x.Gender)
                .Must(gender=>gender==1 || gender==2).WithMessage("Giới tính phải là nam hoặc nữ");
            RuleFor(x => x.PositionId)
                .NotEmpty().WithMessage("Chức vụ không được để trống");
            RuleFor(x => x.Photo)
                .Length(0, 500).WithMessage("Link ảnh không được vượt quá 255 ký tự");
            RuleFor(x => x.SkypeAccount)
                .Length(0, 255).WithMessage("SkypeAccount không được vượt quá 255 ký tự");


        }
        public bool validGender(int gender)
        {
            return gender == 1 || gender == 2;
        }
    }
}
