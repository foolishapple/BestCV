using FluentValidation;
using BestCV.Application.Models.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Slides
{
    public class InsertSlideValidator : AbstractValidator<InsertSlideDTO>
    {
        public InsertSlideValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên có độ dài tối đa là 255 ký tự.");
            RuleFor(c => c.Image).NotEmpty().WithMessage("Đường dẫn ảnh không được để trống")
                .MaximumLength(500).WithMessage("Đường dẫn ảnh có độ dài tối đa là 500 ký tự.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
