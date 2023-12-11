using FluentValidation;
using BestCV.Application.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Roles
{
    public class InsertRoleValidator : AbstractValidator<InsertRoleDTO>
    {
        public InsertRoleValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Tên không được để trống.")
               .MaximumLength(255)
               .WithMessage("Tên không được vượt quá 255 ký tự")
               .Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\-\s]+$")
               .WithMessage("Tên không được chứa số và ký tự đặc biệt");
            RuleFor(c => c.Code).NotEmpty().WithMessage("Mã CODE không được để trống")
                .MaximumLength(255).WithMessage("Mã CODE có độ dài tối đa là 255 ký tự");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự");
        }
    }
}
