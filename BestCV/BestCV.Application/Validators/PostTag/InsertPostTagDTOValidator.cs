using FluentValidation;
using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.PostTag
{
    public class InsertPostTagDTOValidator : AbstractValidator<InsertPostTagDTO>
    {
        public InsertPostTagDTOValidator()
        {
            RuleFor(x => x.PostId)
                .NotNull().WithMessage("Mã bài viết không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã  bài viết phải lớn hơn 0.");   
            RuleFor(x => x.TagId)
                .NotNull().WithMessage("Mã tag bài viết không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã tag bài viết phải lớn hơn 0.");
        }
    }
}
