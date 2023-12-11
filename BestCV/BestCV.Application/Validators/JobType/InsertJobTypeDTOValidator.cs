using FluentValidation;
using BestCV.Application.Models.JobType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.JobType
{
    public class InsertJobTypeDTOValidator :AbstractValidator<InsertJobTypeDTO>
    {
        public InsertJobTypeDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống ");
            RuleFor(x => x.Name).Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt.");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên không được dài quá 255 ký tự. ");
            RuleFor(x => x.Description) .MaximumLength(500).WithMessage("Mô tả không được dài quá 500 ký tự. ");
        }
    }
}
