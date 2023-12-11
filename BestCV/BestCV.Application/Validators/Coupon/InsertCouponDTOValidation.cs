using FluentValidation;
using BestCV.Application.Models.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Coupon
{
	public class InsertCouponDTOValidation : AbstractValidator<InsertCouponDTO>
	{
		public InsertCouponDTOValidation()
		{
			RuleFor(x => x.Code).NotEmpty().WithMessage("Mã code không được để trống.")
				.MaximumLength(50).WithMessage("Mã code không được vượt quá 50 ký tự")
				.Matches(@"^[a-zA-Z]+$").WithMessage("Mã code không được chứa số và ký tự đặc biệt");
			RuleFor(x => x.CouponTypeId).NotNull().WithMessage("Mã loại coupon không được để trống");
		}
	}
}
