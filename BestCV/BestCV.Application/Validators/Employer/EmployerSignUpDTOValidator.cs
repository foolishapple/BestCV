using FluentValidation;
using BestCV.Application.Models.Employer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Employer
{
    public class EmployerSignUpDTOValidator : AbstractValidator<EmployerSignUpDTO>
    {
        public EmployerSignUpDTOValidator()
        {
            RuleFor(x => x.Phone)
                .Matches(@"([\\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})")
                .WithMessage("Số điện thoại chỉ gồm các chữ số & phải đúng số điện thoại Việt Nam");
            RuleFor(x=> x.Password)
                .Length(6,255)
                .WithMessage("Mật khẩu phải nhiều hơn 6 ký tự & không được vượt quá 255 ký tự");
            RuleFor(x => x.ConfirmPassword)
                .Matches(x => x.Password)
                .WithMessage("Mật khẩu nhập lại không đúng với mật khẩu");
            RuleFor(x=> x.Email)
                .Length(1,255)
                .WithMessage("Email không được để trống & không được vượt quá 255 ký tự");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email không đúng định dạng");
            RuleFor(x=> x.Fullname)
                .Length(1,255)
                .WithMessage("Họ tên không được để trống & không được vượt quá 255 ký tự");
            RuleFor(x => x.Fullname)
                .Matches(@"^[a-zA-Z aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ]*$")
                .WithMessage("Họ tên không được có chữ số & ký tự đặc biệt");
            RuleFor(x => x.Gender)
                .NotNull()
                .WithMessage("Giới tính không được để trống");
            RuleFor(x => x.PositionId)
                .NotNull()
                .WithMessage("Vị trí công tác không được để trống");
        }
    }
}
