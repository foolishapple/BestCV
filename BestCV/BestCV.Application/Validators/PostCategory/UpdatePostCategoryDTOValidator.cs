using FluentValidation;
using BestCV.Application.Models.PostCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.PostCategory
{
    public class UpdatePostCategoryDTOValidator : AbstractValidator<UpdatePostCategoryDTO>
    {
        public UpdatePostCategoryDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Mã danh mục bài viết không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã danh mục bài viết phải lớn hơn 0.");
            RuleFor(x => x.Name)
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.")
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên không được dài quá 255 ký tự.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Màu không được để trống.")
                .MaximumLength(12).WithMessage("Màu không được dài quá 12 ký tự.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự.");
        }
    }
}
