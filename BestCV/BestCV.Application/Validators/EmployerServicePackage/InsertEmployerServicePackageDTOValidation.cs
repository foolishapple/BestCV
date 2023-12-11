using FluentValidation;
using BestCV.Application.Models.EmployerServicePackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerServicePackage
{
    public class InsertEmployerServicePackageDTOValidation : AbstractValidator<InsertEmployerServicePackageDTO>
    {
        public InsertEmployerServicePackageDTOValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống.")
                .MaximumLength(255)
                .WithMessage("Tên không được vượt quá 255 ký tự")
                .Matches(@"^[a-z0-9A-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\-\s\&]+$")
                .WithMessage("Tên không được chứa ký tự đặc biệt");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự");
            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Giá tiền không được để trống")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Giá tiền phải lớn hơn hoặc bằng 0");
            RuleFor(x => x.DiscountPrice)
                .NotNull()
                .WithMessage("Giá đã giảm không được để trống")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Giá đã giảm phải lớn hơn hoặc bằng 0");
            RuleFor(x => x.ExpiryTime)
                .NotNull()
                .WithMessage("Thời gian hết hạn không được để trống")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Thời gian hết hạn phải lớn hơn hoặc bằng 0");
        }
    }
}
