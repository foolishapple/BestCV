using FluentValidation;
using BestCV.Application.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Tag
{
    public class UpdateTagDTOValidator : AbstractValidator<UpdateTagDTO>
    {
        public UpdateTagDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Mã Tag không được bỏ trống.")
                .GreaterThan(0).WithMessage("Mã Tag phải lớn hơn 0.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.")
                .MaximumLength(255).WithMessage("Tên không được dài quá 255 ký tự.");
        }
    }
}
