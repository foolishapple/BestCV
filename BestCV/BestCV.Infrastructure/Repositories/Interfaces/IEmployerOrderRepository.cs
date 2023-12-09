using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Aggregates.EmployerOrder;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BestCV.Core.Utilities.LinqExtensions;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerOrderRepository : IRepositoryBaseAsync<EmployerOrder, long, JobiContext>
    {
        Task<object> ListOrderAggregates(DTParameters parameters);
        Task<List<SelectListItem>> ListOrderStatusSelected();
        Task<List<SelectListItem>> ListPaymentMethodSelected();
        Task<List<string>> GetEmployerFullnamesByOrderIdAsync(long orderId);
        Task<bool> QuickIsApprovedAsync(long id);
        Task<List<EmployerOrderDetailAggregates>> ListOrderDetailByOrderId(long id);
        Task UpdateInfoOrder(EmployerOrder order);
        Task<object> PagingByEmployerId(DTPagingEmployerOrderParameters parameters);
        Task<EmployerOrderAggregates> DetailByOrderId(long orderId);
        Task<bool> CancelOrder(long id);
    }
}
