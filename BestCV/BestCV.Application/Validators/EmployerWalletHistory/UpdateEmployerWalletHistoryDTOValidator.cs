using FluentValidation;
using BestCV.Application.Models.EmployerWalletHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerWalletHistory
{
    public class UpdateEmployerWalletHistoryDTOValidator : AbstractValidator<UpdateEmployerWalletHistoryDTO>
    {
        public UpdateEmployerWalletHistoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống.")
                .MaximumLength(255)
                .WithMessage("Tên không được vượt quá 255 ký tự")
                //.Must(BeValidName)
                .Matches(@"^[a-z0-9A-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\-\s]+$")
                .WithMessage("Tên không được chứa ký tự đặc biệt");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Ghi chú không được vượt quá 500 ký tự");
            RuleFor(c => c.Amount).NotEmpty().WithMessage("Giá trị không được để trống.");
            RuleFor(c => c.EmployerWalletId).NotEmpty().WithMessage("Mã ví không được để trống.");
            RuleFor(c => c.WalletHistoryTypeId).NotEmpty().WithMessage("Loại lịch sử giao dịch không được để trống.");
            RuleFor(c => c.CandidateId).NotEmpty().WithMessage("Mã ứng viên không được để trống.");
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã không được để trống.");
        }
    }
}
