using FluentValidation;
using BestCV.Application.Models.UploadFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.UploadFiles
{
	public class InsertUploadFileValidator : AbstractValidator<InsertUploadFileDTO>
	{
        public InsertUploadFileValidator()
        {
            RuleFor(c => c.Files).NotEmpty().WithMessage("Tên tin đẩy lên không được để trống.");
        }
    }
}
