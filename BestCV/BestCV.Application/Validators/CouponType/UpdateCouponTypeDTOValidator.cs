using FluentValidation;
using FluentValidation.Validators;
using BestCV.Application.Models.CouponType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CouponType
{
	public class UpdateCouponTypeDTOValidator : AbstractValidator<UpdateCouponTypeDTO>
	{
		public UpdateCouponTypeDTOValidator()
		{
			RuleFor(x => x.Id).NotNull().WithMessage("Mã không được để trống.")
				.GreaterThan(0).WithMessage("Mã phải lớn hơn 0");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
				.MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự")
				.Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\-\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt");
			RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự");
		}
	}
}
