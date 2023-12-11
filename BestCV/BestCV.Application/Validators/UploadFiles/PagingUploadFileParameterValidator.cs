using FluentValidation;
using BestCV.Domain.Aggregates.UploadFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.UploadFiles
{
	public class PagingUploadFileParameterValidator : AbstractValidator<PagingUploadFileParameter>
	{
        public PagingUploadFileParameterValidator()
        {
            RuleFor(c => c.FodlderUploadId).NotEmpty().WithMessage("Mã thư mục cha không được để trống.");
        }
    }
}
