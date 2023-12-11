using FluentValidation;
using BestCV.Application.Models.EmployerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerOrders
{
    public class UpdateEmployerOrderStatusDTOValidator : AbstractValidator<UpdateEmployerOrderStatusDTO>
    {
        public UpdateEmployerOrderStatusDTOValidator()
        {
            RuleFor(c => c.OrderId).NotEmpty().WithMessage("Mã đơn hàng không được để trống");
            RuleFor(c => c.OrderStatusId).NotEmpty().WithMessage("Mã trạng thái đơn hàng không được để trống");
        }
    }
}
