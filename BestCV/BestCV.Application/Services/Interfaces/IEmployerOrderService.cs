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
        Task<DionResponse> QuickIsApprovedAsync(long id);
        Task<DionResponse> AdminDetailAsync(long id);
        Task<EmployerOrderAndOrderDetailDTO> ListOrderDetailByOrderId(long id);
        Task<DionResponse> UpdateInfoOrder(UpdateInfoOrderDTO model);
        Task<DionResponse> AddOrder(CreateEmployerOrderDTO model);
        Task<DionResponse> ListByEmployerId(long employerId);
        Task<object> PagingByEmployerId(DTPagingEmployerOrderParameters parameters);
        Task<DionResponse> DetailByOrderId(long orderId);
        Task<DionResponse> CancelOrder(long orderId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/09/2023
        /// Description: Update order status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> UpdateOrderStatus(UpdateEmployerOrderStatusDTO obj);
    }
}
