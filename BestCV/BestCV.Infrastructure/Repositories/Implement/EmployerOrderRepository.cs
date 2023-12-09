using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Aggregates.EmployerOrder;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BestCV.Core.Utilities.LinqExtensions;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerOrderRepository : RepositoryBaseAsync<EmployerOrder, long, JobiContext>, IEmployerOrderRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerOrderRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            _db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 12/9/2023
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetEmployerFullnamesByOrderIdAsync(long orderId)
        {
            var query =
                from e in _db.Employers
                join eo in _db.EmployerOrders on e.Id equals eo.EmployerId
                where eo.Id == orderId && e.Active && eo.Active
                select e.Fullname;

            return await query.ToListAsync();
        }


        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<object> ListOrderAggregates(DTParameters parameters)
        {
            var keyword = parameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            if (parameters.Order != null)
            {
                orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            var result = await (
                from o in _db.EmployerOrders
                //join od in _db.EmployerOrderDetails on o.Id equals od.OrderId
                join e in _db.Employers on o.EmployerId equals e.Id
                join c in _db.Companies on e.Id equals c.EmployerId
                join os in _db.OrderStatuses on o.OrderStatusId equals os.Id
                join pm in _db.PaymentMethods on o.PaymentMethodId equals pm.Id
                where o.Active /*&& od.Active*/ && e.Active && os.Active && pm.Active
                select new EmployerOrderAggregates
                {
                    Id = o.Id,
                    OrderStatusId = o.OrderStatusId,
                    OrderStatusName = os.Name,
                    EmployerId = o.EmployerId,
                    EmployerName = e.Fullname,
                    PaymentMethodId = o.PaymentMethodId,
                    PaymentMethodName = pm.Name,
                    Search = o.Search,
                    CreatedTime = o.CreatedTime,
                    DiscountPrice = o.DiscountPrice,
                    DiscountVoucher = o.DiscountVoucher,
                    Active = o.Active,
                    FinalPrice = o.FinalPrice,
                    Price = o.Price,
                    Info = o.Info,
                    RequestId = o.RequestId,
                    TransactionCode = o.TransactionCode,
                    IsApproved = o.IsApproved,
                    CompanyId = c.Id,
                    CompanyName = c.Name,
                    OrderStatusColor = os.Color
                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword)
            || s.OrderStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.Price.ToString().Contains(keyword.ToLower().RemoveVietnamese())
            || s.DiscountPrice.ToString().Contains(keyword.ToLower().RemoveVietnamese())
            || s.DiscountVoucher.ToString().Contains(keyword.ToLower().RemoveVietnamese())
            || s.FinalPrice.ToString().Contains(keyword.ToLower().RemoveVietnamese())
            || s.EmployerName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.PaymentMethodName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())
            || s.CreatedTime.ToCustomString().Contains(keyword)
            || (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))).ToList();

            foreach (var column in parameters.Columns)
            {
                var search = column.Search.Value;
                if (!search.Contains("/"))
                {
                    search = column.Search.Value.ToLower().RemoveVietnamese();
                }
                if (string.IsNullOrEmpty(search)) continue;
                switch (column.Data)
                {
                    case "employerName":
                        result = result.Where(r => r.EmployerName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "orderStatusName":
                        var orderStatusIdArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => orderStatusIdArr.Contains(r.OrderStatusId)).ToList();
                        break;
                    case "paymentMethodName":
                        var paymentMethodIdArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => paymentMethodIdArr.Contains(r.PaymentMethodId)).ToList();
                        break;
                    case "price":
                        result = result.Where(r => r.Price.ToString().Contains(search)).ToList();
                        break;
                    case "discountPrice":
                        result = result.Where(r => r.DiscountPrice.ToString().Contains(search)).ToList();
                        break;
                    case "finalPrice":
                        result = result.Where(r => r.FinalPrice.ToString().Contains(search)).ToList();
                        break;
                    case "discountVoucher":
                        result = result.Where(r => r.DiscountVoucher.ToString().Contains(search)).ToList();
                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;
                    case "isApproved":
                        if (search != "null")
                        {
                            result = result.Where(r => r.IsApproved.ToString().ToLower().Contains(search)).ToList();
                        }
                        break;
                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();
            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                allData = result,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 12/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<EmployerOrderDetailAggregates>> ListOrderDetailByOrderId(long id)
        {
            var result = await (
                from od in _db.EmployerOrderDetails
                join o in _db.EmployerOrders on od.OrderId equals o.Id
                join ep in _db.EmployerServicePackages on od.EmployerServicePackageId equals ep.Id
                where od.OrderId == id && od.Active && o.Active && ep.Active
                select new EmployerOrderDetailAggregates
                {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    EmployerServicePackageId = od.EmployerServicePackageId,
                    EmployerServicePackageName = ep.Name,
                    Price = od.Price,
                    DiscountPrice = od.DiscountPrice,
                    FinalPrice = od.FinalPrice,
                    CreatedTime = od.CreatedTime,
                    Quantity = od.Quantity,

                }).ToListAsync();
            return result;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListOrderStatusSelected()
        {
            return await _db.OrderStatuses.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }
        
        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListPaymentMethodSelected()
        {
            return await _db.PaymentMethods.Where(c => c.Active).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> QuickIsApprovedAsync(long id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.IsApproved = obj.IsApproved ? false : true;

                _db.Attach(obj);
                _db.Entry(obj).Property(x => x.IsApproved).IsModified = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 12/9/2023
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateInfoOrder(EmployerOrder order)
        {
            _db.Attach(order);
            _db.Entry(order).Property(c => c.Info).IsModified = true;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 14/09/2023
        /// Description : Danh sach don hang voi employerId
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> PagingByEmployerId(DTPagingEmployerOrderParameters parameters)
        {
            //0.Opttion
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            //1.Query LINQ
            var query = await( from eo in _db.EmployerOrders
                        join e in _db.Employers on eo.EmployerId equals e.Id
                        join os in _db.OrderStatuses on eo.OrderStatusId equals os.Id
                        join pm in _db.PaymentMethods on eo.PaymentMethodId equals pm.Id
                        where eo.Active && e.Active && os.Active && pm.Active && eo.EmployerId == parameters.EmployerId
                        orderby eo.CreatedTime descending
                        select new EmployerOrderAggregates()
                        {
                            Id = eo.Id,
                            Active = eo.Active,
                            PaymentMethodId = eo.PaymentMethodId,
                            OrderStatusId = eo.OrderStatusId,
                            EmployerId = eo.EmployerId,
                            ApplyEndDate = eo.ApplyEndDate,
                            CreatedTime = eo.CreatedTime,
                            DiscountPrice = eo.DiscountPrice,
                            EmployerName = e.Fullname,
                            DiscountVoucher = eo.DiscountVoucher,
                            FinalPrice = eo.FinalPrice,
                            Info = eo.Info,
                            IsApproved = eo.IsApproved,
                            OrderStatusName = os.Name,
                            PaymentMethodName = pm.Name,
                            Price = eo.Price,
                            RequestId = eo.RequestId,
                            Search = eo.Search,
                            TransactionCode = eo.TransactionCode,
                            OrderStatusColor = os.Color,
                        }).ToListAsync();

            int recordsTotal = query.Count;
            //Filter
            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(c => c.EmployerName.RemoveVietnamese().ToLower().Contains(noneVietnameseKeyword)
                || c.OrderStatusName.RemoveVietnamese().ToLower().Contains(noneVietnameseKeyword)
                || c.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword)
                || ("#" + c.Id.ToString()).Contains(noneVietnameseKeyword)
                || c.FinalPrice.ToString().Contains(noneVietnameseKeyword)
                || c.IsApproved ? "da duyet".Contains(noneVietnameseKeyword) : "chua duyet".Contains(noneVietnameseKeyword)).ToList();
            }


            int recordsFiltered =  query.Count;
            //3.Sort
            //4.Return data
            var data = query.Skip(parameters.Start).Take(parameters.Length).ToList();
            DTResult<EmployerOrderAggregates> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }

        public async Task<EmployerOrderAggregates> DetailByOrderId(long orderId)
        {

            //1.Query LINQ
            var query = from eo in _db.EmployerOrders
                        join e in _db.Employers on eo.EmployerId equals e.Id
                        join os in _db.OrderStatuses on eo.OrderStatusId equals os.Id
                        join pm in _db.PaymentMethods on eo.PaymentMethodId equals pm.Id
                        where eo.Active && e.Active && os.Active && pm.Active && eo.Id == orderId
                        orderby eo.CreatedTime descending
                        select new EmployerOrderAggregates()
                        {
                            Id = eo.Id,
                            Active = eo.Active,
                            PaymentMethodId = eo.PaymentMethodId,
                            OrderStatusId = eo.OrderStatusId,
                            EmployerId = eo.EmployerId,
                            ApplyEndDate = eo.ApplyEndDate,
                            CreatedTime = eo.CreatedTime,
                            DiscountPrice = eo.DiscountPrice,
                            EmployerName = e.Fullname,
                            DiscountVoucher = eo.DiscountVoucher,
                            FinalPrice = eo.FinalPrice,
                            Info = eo.Info,
                            IsApproved = eo.IsApproved,
                            OrderStatusName = os.Name,
                            PaymentMethodName = pm.Name,
                            Price = eo.Price,
                            RequestId = eo.RequestId,
                            Search = eo.Search,
                            TransactionCode = eo.TransactionCode,
                            OrderStatusColor = os.Color,
                            ListOrderDetail = (from od in _db.EmployerOrderDetails
                                               join o in _db.EmployerOrders on od.OrderId equals o.Id
                                               join s in _db.EmployerServicePackages on od.EmployerServicePackageId equals s.Id
                                               where od.Active && o.Active && s.Active && od.OrderId == orderId
                                               select new EmployerOrderDetailAggregates()
                                               {
                                                   Id = od.Id,
                                                   OrderId = od.OrderId,
                                                   CreatedTime = od.CreatedTime,
                                                   DiscountPrice = od.DiscountPrice,
                                                   EmployerServicePackageId = od.EmployerServicePackageId,
                                                   FinalPrice = od.FinalPrice,
                                                   Price = od.Price,
                                                   Quantity = od.Quantity,
                                                   EmployerServicePackageName = s.Name
                                               }).ToList(),
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> CancelOrder(long id)
        {
            var data = await _db.EmployerOrders.Where(x => x.Active && x.Id == id).FirstOrDefaultAsync();
            if(data == null)
            {
                return false;
            }
            data.OrderStatusId = OrderStatusID.DA_HUY;
            _db.Attach(data);
            _db.Entry(data).Property(c => c.OrderStatusId).IsModified = true;
            return true;
        }
    }
}
