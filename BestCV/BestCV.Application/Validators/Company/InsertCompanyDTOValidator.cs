using FluentValidation;
using BestCV.Application.Models.Company;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Company
{
    public class InsertCompanyDTOValidator : AbstractValidator<InsertCompanyDTO>
    {
        public InsertCompanyDTOValidator()
        {
            RuleFor(x => x.Name)
                 .NotNull()
                 .WithMessage("Tên công ty không được để trống")
                 .Length(0, 255)
                 .WithMessage("Tên công ty không được vượt quá 255 ký tự");
            RuleFor(x => x.Name)
                 .Matches(@"^[a-zA-Z\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]+$")
                 .WithMessage("Tên không được chứa ký tự đặc biệt.");
            RuleFor(x => x.EmailAddress)
                 .NotNull()
                 .WithMessage("Địa chỉ chi tiết không được để trống")
                 .Length(0, 500)
                 .WithMessage("Địa chỉ chi tiết không được quá 500 ký tự");
            RuleFor(x => x.Phone)
                .NotNull()
                .WithMessage("Số điện thoại không được để trống")
                .NotEmpty()
                .WithMessage("Số điện thoại không được để trống")
                .Length(10)
                .WithMessage("Số điện thoại phải có đúng 10 chữ số")
                .Matches("^[0-9]+$")
                .WithMessage("Số điện thoại không được chứa ký tự không phải số");
            RuleFor(x => x.AddressDetail)
                 .NotNull()
                 .WithMessage("Địa chỉ chi tiết không được để trống")
                 .Length(0, 500)
                 .WithMessage("Địa chỉ chi tiết không được quá 500 ký tự");
            RuleFor(x => x.Location)
                 .Length(0, 255)
                 .WithMessage("Tọa độ không được vượt quá 500 ký tự");
            RuleFor(x => x.Website)
                 .NotNull()
                 .WithMessage("Website không được để trống")
                 .Length(1, 500)
                 .WithMessage("Website không được quá 500 ký tự");
            RuleFor(x => x.Logo)
                 .NotNull()
                 .WithMessage("Ảnh đại diện không được để trống")
                 .Length(1, 500)
                 .WithMessage("Ảnh đại diện không được quá 500 ký tự");
            RuleFor(x => x.CoverPhoto)
                 .NotNull()
                 .WithMessage("Ảnh bìa không được để trống")
                 .Length(1, 500)
                 .WithMessage("Ảnh bìa không được quá 500 ký tự");
            RuleFor(x => x.TaxCode)
                 .NotNull()
                 .WithMessage("Mã số thuế không được để trống")
                 .Length(1, 50)
                 .WithMessage("Mã số thuế không được quá 50 ký tự");
            RuleFor(x => x.FoundedIn)
                 .NotNull()
                 .WithMessage("Năm thành lập không được để trống")
                 .GreaterThan(0)
                 .WithMessage("Năm thành lập phải lơn hơn 0");
            RuleFor(x => x.TiktokLink)
                 .Length(0, 500)
                 .WithMessage("TiktokLink không được quá 500 ký tự");
            RuleFor(x => x.YoutubeLink)
                 .Length(0, 500)
                 .WithMessage("YoutubeLink không được quá 500 ký tự");
            RuleFor(x => x.FacebookLink)
                 .Length(0, 500)
                 .WithMessage("FacebookLink không được quá 500 ký tự");
            RuleFor(x => x.LinkedinLink)
                 .Length(0, 500)
                 .WithMessage("LinkedinLink không được quá 500 ký tự");
            RuleFor(x => x.TwitterLink)
                 .Length(0, 500)
                 .WithMessage("TwitterLink không được quá 500 ký tự");
            RuleFor(x => x.VideoIntro)
                 .Length(0, 500)
                 .WithMessage("VideoIntro không được quá 500 ký tự");
            RuleFor(x => x.WorkPlaceId)
                 .NotEmpty()
                 .WithMessage("Vị trí công ty không được để trống");
            RuleFor(x => x.WorkPlaceId)
                 .NotNull()
                 .WithMessage("Địa chỉ Tình/thành phố không được để trống");
            RuleFor(x => x.Overview)
                .Length(1, 255)
                .WithMessage("Giới thiệu công ty không được quá 255 ký tự");
        }
    }
}
