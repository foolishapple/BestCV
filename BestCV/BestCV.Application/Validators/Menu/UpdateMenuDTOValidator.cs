using FluentValidation;
using BestCV.Application.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Menu
{
    public class UpdateMenuDTOValidator : AbstractValidator<UpdateMenuDTO>
    {
        public UpdateMenuDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Mã không được để trống.")
                .GreaterThan(0)
                .WithMessage("Mã phải lớn hơn 0");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống.")
                .MaximumLength(255)
                .WithMessage("Tên không được vượt quá 255 ký tự")
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\-\s]+$")
                .WithMessage("Tên không được chứa số và ký tự đặc biệt");

        }
    }
}
