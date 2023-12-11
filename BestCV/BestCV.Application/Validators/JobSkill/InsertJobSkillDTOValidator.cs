using FluentValidation;
using BestCV.Application.Models.JobSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.JobSkill
{
	public class InsertJobSkillDTOValidator : AbstractValidator<InsertJobSkillDTO>
	{
		public InsertJobSkillDTOValidator()
		{
			RuleFor(x => x.Name).NotEmpty()
				.WithMessage("Tên không được để trống.")
				.Matches(@"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-,\s]+$").WithMessage("Tên không được chứa số hoặc ký tự đặc biệt")
				.MaximumLength(255)
				.WithMessage("Tên không được vượt quá 255 ký tự");
			RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự");
		}

	}
}
