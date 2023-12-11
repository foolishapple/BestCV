using FluentValidation;
using FluentValidation.Validators;
using BestCV.Application.Models.Occupation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Occupation
{
    public class UpdateOccupationDTOValidator : AbstractValidator<UpdateOccupationDTO>
    {
        public UpdateOccupationDTOValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự")
            .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-,\s]+$")
            .WithMessage("Tên không được chứa số và ký tự đặc biệt");

            //RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả Không được để trống")
            //.MaximumLength(500).WithMessage("Mô tả có dộ dài tối đa là 500 ký tự")
            //.Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-,\s]+$")
            //.WithMessage("Tên không được chứa số và ký tự đặc biệt");
        }
    }
}
