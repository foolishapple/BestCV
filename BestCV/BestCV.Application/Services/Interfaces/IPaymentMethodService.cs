using BestCV.Application.Models.PaymentMethod;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IPaymentMethodService : IServiceQueryBase<int, InsertPaymentMethodDTO, UpdatePaymentMethodDTO, PaymentMethodDTO>
    { 

    }
}
