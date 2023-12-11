using AutoMapper;
using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.Employer;
using BestCV.Application.Models.EmployerOrder;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerOrder;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class EmployerOrderService : IEmployerOrderService
    {
        private readonly IEmployerOrderRepository employerOrderRepository;
        private readonly IEmployerOrderDetailRepository employerOrderDetailRepository;
        private readonly IEmployerCartRepository employerCartRepository;
        private readonly IEmployerServicePackageEmployerRepository _employerServicePackageEmployerRepository;
        private readonly IServicePackageBenefitRepository _servicePackageBenefitRepository;
        private readonly IEmployerWalletRepository _employerWalletRepository;
        private readonly ILogger<EmployerOrderService> logger;
        private readonly IMapper mapper;
        public EmployerOrderService(
            IEmployerOrderRepository _employerOrderRepository,
            IEmployerOrderDetailRepository _employerOrderDetailRepository,
            ILoggerFactory loggerFactory,
            IEmployerCartRepository _employerCartRepository,
            IMapper _mapper
,
            IEmployerServicePackageEmployerRepository employerServicePackageEmployerRepository,
            IServicePackageBenefitRepository servicePackageBenefitRepository,
            IEmployerWalletRepository employerWalletRepository)
        {
            employerOrderRepository = _employerOrderRepository;
            logger = loggerFactory.CreateLogger<EmployerOrderService>();
            mapper = _mapper;
            employerOrderDetailRepository = _employerOrderDetailRepository;
            employerCartRepository = _employerCartRepository;
            _employerServicePackageEmployerRepository = employerServicePackageEmployerRepository;
            _servicePackageBenefitRepository = servicePackageBenefitRepository;
            _employerWalletRepository = employerWalletRepository;
        }

        public Task<DionResponse> CreateAsync(InsertEmployerOrderDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertEmployerOrderDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ListOrderAggregates(DTParameters parameters)
        {
            return await employerOrderRepository.ListOrderAggregates(parameters);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListOrderStatusSelected()
        {
            return await employerOrderRepository.ListOrderStatusSelected();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListPaymentMethodSelected()
        {
            return await employerOrderRepository.ListPaymentMethodSelected();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> QuickIsApprovedAsync(long id)
        {
            using (var trans = await employerOrderRepository.BeginTransactionAsync())
            {
                try
                {
                    var order = await employerOrderRepository.GetByIdAsync(id);
                    if (order != null)
                    {
                        if (!order.IsApproved)
                        {
                            order.ApprovalDate = DateTime.Now;
                            if (order.OrderStatusId == OrderStatusID.DA_THANH_TOAN && !(await _employerServicePackageEmployerRepository.CheckServiceAdded(id)))//Kiểm tra xem đã thêm dịch vụ hay chưa
                            {
                                #region Add service package
                                var orderDetails = await employerOrderDetailRepository.FindByConditionAsync(c => c.Active && c.OrderId == id);
                                if (orderDetails != null && orderDetails.Count > 0)
                                {
                                    var employerServicePackageEmployers = new List<EmployerServicePackageEmployer>();
                                    foreach (var od in orderDetails)
                                    {
                                        for (int i = 0; i < od.Quantity; i++)
                                        {
                                            var service = new EmployerServicePackageEmployer()
                                            {
                                                Active = true,
                                                CreatedTime = DateTime.Now,
                                                EmployerOrderDetailId = od.Id
                                            };
                                            employerServicePackageEmployers.Add(service);
                                        }

                                        //nếu mua gói Credit
                                        if (od.EmployerServicePackageId == ServicePackageConst.CREDIT_1 || od.EmployerServicePackageId == ServicePackageConst.CREDIT_2)
                                        {
                                            //tìm ví, nếu có thì cộng value, k có thì thêm mới
                                            var wallet = await _employerWalletRepository.FindByConditionAsync(x => x.Active && x.WalletTypeId == EmployerWalletConstants.CREDIT_TYPE && x.EmployerId == order.EmployerId);
                                            if (wallet.Count == 0)
                                            {
                                                var newWallet = new EmployerWallet()
                                                {
                                                    Active = true,
                                                    EmployerId = order.EmployerId,
                                                    WalletTypeId = EmployerWalletConstants.CREDIT_TYPE,
                                                    CreatedTime = DateTime.Now,
                                                    Value = 0,
                                                };
                                                //wallet[0].Active = true;
                                                //wallet[0].EmployerId = order.EmployerId;
                                                //wallet[0].WalletTypeId = EmployerWalletConstants.CREDIT_TYPE;
                                                //wallet[0].CreatedTime = DateTime.Now;
                                                //wallet[0].Value = 0;
                                                await _employerWalletRepository.CreateAsync(newWallet);
                                                await _employerWalletRepository.SaveChangesAsync();
                                                wallet.Add(newWallet);
                                            }

                                            //lấy danh sách benefit
                                            var listBenefit = await _servicePackageBenefitRepository.FindByConditionAsync(x => x.Active && x.EmployerServicePackageId == od.EmployerServicePackageId);
                                            if(listBenefit != null)
                                            {
                                                var creditValue = 0;
                                                foreach (var b in listBenefit)
                                                {
                                                    if(b.Value != null && b.BenefitId == BenefitId.QUA_TANG_CREDIT)
                                                    {
                                                        creditValue += b.Value.Value;
                                                    }
                                                }
                                                wallet[0].Value += creditValue;
                                                await _employerWalletRepository.UpdateAsync(wallet[0]);
                                                await _employerWalletRepository.SaveChangesAsync();

                                            }
                                        }
                                    };
                                    await _employerServicePackageEmployerRepository.CreateListAsync(employerServicePackageEmployers);
                                    if(!((await _employerServicePackageEmployerRepository.SaveChangesAsync()) > 0))
                                    {
                                        return DionResponse.BadRequest(new string[] { "Kích hoạt đơn hàng không thành công" });
                                    }
                                }
                                #endregion
                            }
                        }
                        #region Approved order
                        order.IsApproved = !order.IsApproved;
                        await employerOrderRepository.UpdateAsync(order);
                        if((await employerOrderRepository.SaveChangesAsync()) > 0)
                        {
                            await trans.CommitAsync();
                            return DionResponse.Success();
                        }
                        #endregion
                    }
                    return DionResponse.BadRequest(new string[] { "Kích hoạt đơn hàng không thành công" });
                }
                catch (Exception e)
                {
                    throw new Exception($"Failed to quick approved order with id: {id}");
                }
            }

        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 11/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> AdminDetailAsync(long id)
        {
            var data = await employerOrderRepository.GetByIdAsync(id);
            if (data != null)
            {
                var model = mapper.Map<EmployerOrderDTO>(data);
                var employerFullnames = await employerOrderRepository.GetEmployerFullnamesByOrderIdAsync(id);

                // Tạo danh sách EmployerDTO từ danh sách Fullname
                model.ListEmployer = employerFullnames.Select(fullname => new EmployerDTO
                {
                    Fullname = fullname
                }).ToList();

                // Gán danh sách EmployerDTO vào model.ListEmployer
                return DionResponse.Success(model);
            }
            return DionResponse.NotFound("Không tìm thấy đơn hàng", data);
        }

        public Task<DionResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateEmployerOrderDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateEmployerOrderDTO> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 12/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<EmployerOrderAndOrderDetailDTO> ListOrderDetailByOrderId(long id)
        {
            var aggregatesList = await employerOrderRepository.ListOrderDetailByOrderId(id);

            var dtoList = aggregatesList.Select(aggregates => new EmployerOrderDetailDTO
            {
                Id = aggregates.Id,
                OrderId = aggregates.OrderId,
                EmployerServicePackageId = aggregates.EmployerServicePackageId,
                EmployerServicePackageName = aggregates.EmployerServicePackageName,
                Quantity = aggregates.Quantity,
                Price = aggregates.Price,
                DiscountPrice = aggregates.DiscountPrice,
                FinalPrice = aggregates.FinalPrice,
                CreatedTime = aggregates.CreatedTime
            }).ToList();

            // Tạo đối tượng DTO chứa danh sách ánh xạ
            var dto = new EmployerOrderAndOrderDetailDTO
            {
                DiscountVoucher = 0,
                ListOrderDetail = dtoList
            };

            return dto;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:12/9/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> UpdateInfoOrder(UpdateInfoOrderDTO model)
        {
            var order = mapper.Map<EmployerOrder>(model);
            await employerOrderRepository.UpdateInfoOrder(order);
            return DionResponse.Success(model);
        }

        public async Task<DionResponse> AddOrder(CreateEmployerOrderDTO model)
        {
            using (var database = await employerOrderRepository.BeginTransactionAsync())
            {
                try
                {
                    var newOrder = mapper.Map<EmployerOrder>(model);
                    newOrder.Active = true;
                    newOrder.CreatedTime = DateTime.Now;
                    newOrder.IsApproved = false;

                    await employerOrderRepository.CreateAsync(newOrder);
                    await employerOrderRepository.SaveChangesAsync();

                    if (model.ListEmployerOrderDetail.Count > 0)
                    {
                        var newOrderDetails = new List<EmployerOrderDetail>();
                        var listItemsInCart = new List<long>();
                        foreach (var od in model.ListEmployerOrderDetail)
                        {
                            var newOD = mapper.Map<EmployerOrderDetail>(od);
                            newOD.Active = true;
                            newOD.CreatedTime = DateTime.Now;
                            newOD.OrderId = newOrder.Id;
                            newOrderDetails.Add(newOD);

                            //tìm kiếm item trong giỏ hàng
                            var itemInCart = await employerCartRepository.FindByConditionAsync(x => x.Active && x.EmployerServicePackageId == od.EmployerServicePackageId && x.EmployerId == model.EmployerId);
                            listItemsInCart.Add(itemInCart[0].Id);
                        }
                        //tạo mới danh sách order detail
                        await employerOrderDetailRepository.CreateListAsync(newOrderDetails);
                        await employerOrderDetailRepository.SaveChangesAsync();

                        //xóa đi những item đã được tạo order
                        await employerCartRepository.SoftDeleteListAsync(listItemsInCart);
                        await employerCartRepository.SaveChangesAsync();

                    }
                    await database.CommitAsync();
                    return DionResponse.Success();
                }
                catch
                {
                    await database.RollbackAsync();
                    throw new Exception();
                }
            }
        }

        public async Task<DionResponse> ListByEmployerId(long employerId)
        {
            var data = await employerOrderRepository.FindByConditionAsync(x => x.Active && x.EmployerId == employerId);
            if (data.Count == 0)
            {
                return DionResponse.NotFound("Không tìm thấy dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<object> PagingByEmployerId(DTPagingEmployerOrderParameters parameters)
        {
            return await employerOrderRepository.PagingByEmployerId(parameters);
        }

        public async Task<DionResponse> DetailByOrderId(long orderId)
        {
            var data = await employerOrderRepository.DetailByOrderId(orderId);
            if (data == null)
            {
                return DionResponse.NotFound("Không tìm thấy dữ liệu", data);

            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> CancelOrder(long orderId)
        {
            var isCanceled = await employerOrderRepository.CancelOrder(orderId);
            if (!isCanceled)
            {
                return DionResponse.BadRequest(isCanceled);
            }
            await employerOrderRepository.SaveChangesAsync();
            return DionResponse.Success(isCanceled);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/09/2023
        /// Description: Update order status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateOrderStatus(UpdateEmployerOrderStatusDTO obj)
        {
            var order = await employerOrderRepository.GetByIdAsync(obj.OrderId);
            if (order == null)
            {
                throw new Exception($"Failed to found with order id {obj.OrderId}");
            }
            order.OrderStatusId = obj.OrderStatusId;
            await employerOrderRepository.UpdateAsync(order);
            if ((await employerOrderRepository.SaveChangesAsync()) > 0)
            {
                return DionResponse.Success();
            }
            throw new Exception($"Failed to updated with order id {obj.OrderId}");
        }
    }
}
