using FluentValidation;
using BestCV.Application.Models.ServicePackageGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.ServicePackageType
{
    public class UpdateServicePackageTypeDTOValidator : AbstractValidator<UpdateServicePackageGroupDTO>
    {
        public UpdateServicePackageTypeDTOValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự")
                .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt");

            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả có dộ dài tối đa là 500 ký tự");
        }
    }
}
