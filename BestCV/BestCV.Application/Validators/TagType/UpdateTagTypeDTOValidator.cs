using FluentValidation;
using BestCV.Application.Models.TagType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.TagType
{
    public class UpdateTagTypeDTOValidator : AbstractValidator<UpdateTagTypeDTO>
    {
        public UpdateTagTypeDTOValidator() {
            RuleFor(x => x.Id).NotNull().WithMessage("Mã loại tag không được để trống. ");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Mã loại tag phải lớn hơn 0. ");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống. ");
            RuleFor(x => x.Name).Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên không được dài quá 255 ký tự. ");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự. ");
        }
    }
}
