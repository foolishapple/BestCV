using FluentValidation;
using BestCV.Application.Models.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.RolePermissions
{
    public class UpdateRolePermissionValidator : AbstractValidator<UpdateRolePermissionDTO>
    {
        public UpdateRolePermissionValidator()
        {
            RuleFor(c => c.RoleId).NotEmpty().WithMessage("Mã vai trò không được để trống.");
            RuleFor(c => c.PermissionId).NotEmpty().WithMessage("Mã quyền không được để trống.");
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã quyền vai trò không được để trống.");
        }
    }
}
