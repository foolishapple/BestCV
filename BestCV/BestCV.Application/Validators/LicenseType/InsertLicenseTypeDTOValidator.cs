using FluentValidation;
using BestCV.Application.Models.LicenseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.LicenseType
{
    public class InsertLicenseTypeDTOValidator : AbstractValidator<InsertLicenseTypeDTO>
    {
        public InsertLicenseTypeDTOValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự")
            .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-,\s]+$")
            .WithMessage("Tên không được chứa số và ký tự đặc biệt");
        }
    }
}
