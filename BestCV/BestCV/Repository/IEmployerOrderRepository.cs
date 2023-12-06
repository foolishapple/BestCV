using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateApplyJobs;
using Jobi.Domain.Aggregates.Employer;
using Jobi.Domain.Aggregates.EmployerOrder;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jobi.Core.Utilities.LinqExtensions;

namespace Jobi.Infrastructure.Repositories.Interfaces
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
