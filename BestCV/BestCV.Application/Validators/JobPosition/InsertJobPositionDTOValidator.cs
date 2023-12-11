using FluentValidation;
using BestCV.Application.Models.JobPosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.JobPosition
{
	public class InsertJobPositionDTOValidator : AbstractValidator<InsertJobPositionDTO>
	{
		public InsertJobPositionDTOValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
				.MaximumLength(255).WithMessage("Tên không được vượt quá 255 ký tự")
				.Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-,\s]+$").WithMessage("Tên không được chứa số và ký tự đặc biệt");
			RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự");
		}
	}
}
