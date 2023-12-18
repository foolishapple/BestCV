using BestCV.Application.Models.EmployerOrder;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerOrder;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BestCV.Core.Utilities.LinqExtensions;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerOrderService : IServiceQueryBase<long, InsertEmployerOrderDTO, UpdateEmployerOrderDTO, EmployerOrderDTO>
    {
        Task<object> ListOrderAggregates(DTParameters parameters);
        Task<List<SelectListItem>> ListOrderStatusSelected();
        Task<List<SelectListItem>> ListPaymentMethodSelected();
        Task<BestCVResponse> QuickIsApprovedAsync(long id);
        Task<BestCVResponse> AdminDetailAsync(long id);
        Task<EmployerOrderAndOrderDetailDTO> ListOrderDetailByOrderId(long id);
        Task<BestCVResponse> UpdateInfoOrder(UpdateInfoOrderDTO model);
        Task<BestCVResponse> AddOrder(CreateEmployerOrderDTO model);
        Task<BestCVResponse> ListByEmployerId(long employerId);
        Task<object> PagingByEmployerId(DTPagingEmployerOrderParameters parameters);
        Task<BestCVResponse> DetailByOrderId(long orderId);
        Task<BestCVResponse> CancelOrder(long orderId);

        Task<BestCVResponse> UpdateOrderStatus(UpdateEmployerOrderStatusDTO obj);
    }
}
