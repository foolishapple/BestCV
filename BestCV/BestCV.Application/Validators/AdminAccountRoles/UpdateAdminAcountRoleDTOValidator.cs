using FluentValidation;
using BestCV.Application.Models.AdminAccountRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.AdminAccountRoles
{
    public class UpdateAdminAcountRoleDTOValidator : AbstractValidator<UpdateAdminAccountRoleDTO>
    {
        public UpdateAdminAcountRoleDTOValidator()
        {
            RuleFor(c => c.RoleId).NotEmpty().WithMessage("Mã vai trò không được để trống.");
            RuleFor(c => c.AdminAccountId).NotEmpty().WithMessage("Mã tài khoản quản trị viên không được để trống.");
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã vai trò tài khoản quản trị viên không được để trống.");
        }
    }
}
