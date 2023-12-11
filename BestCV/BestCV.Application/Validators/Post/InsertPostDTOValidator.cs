using FluentValidation;
using BestCV.Application.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Post
{
    public class InsertPostDTOValidator : AbstractValidator<InsertPostDTO>
    {
        public InsertPostDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(500).WithMessage("Tên không được dài quá 500 ký tự.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Nội dung bài viết không được để trống.");

            RuleFor(x => x.Overview)
                .NotEmpty().WithMessage("Tóm tắt không được để trống.")
                .MaximumLength(500).WithMessage("Tóm tắt không được dài quá 500 ký tự.");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Ảnh bài viết không được để trống.")
                .MaximumLength(500).WithMessage("Đường dẫn ảnh không được dài quá 500 ký tự.");
        }
    }
}
