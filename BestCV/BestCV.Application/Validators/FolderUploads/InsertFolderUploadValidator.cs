using FluentValidation;
using BestCV.Application.Models.FolderUploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.FolderUploads
{
	public class InsertFolderUploadValidator : AbstractValidator<InsertFolderUploadDTO>
	{
        public InsertFolderUploadValidator()
        {
            RuleFor(c => c.Name).MaximumLength(255).WithMessage("Tên thư mục có độ dài tối đa là 255 ký tự.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
