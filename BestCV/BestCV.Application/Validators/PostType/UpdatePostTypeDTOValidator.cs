using FluentValidation;
using BestCV.Application.Models.PostType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.PostType
{
    public class UpdatePostTypeDTOValidator : AbstractValidator<UpdatePostTypeDTO>
    {
        public UpdatePostTypeDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Mã loại bài viết không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã loại bài viết phải lớn hơn 0.");
            RuleFor(x => x.Name)
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.")
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên không được dài quá 255 ký tự.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự.");
        }
    }
}
