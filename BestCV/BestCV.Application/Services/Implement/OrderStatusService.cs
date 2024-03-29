using AutoMapper;
using BestCV.Application.Models.NotificationType;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly ILogger<IOrderStatusService> logger;
        private readonly IMapper mapper;
        public OrderStatusService(IOrderStatusRepository orderStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<OrderStatusService>();
            this.orderStatusRepository = orderStatusRepository;
        }


        public async Task<BestCVResponse> CreateAsync(InsertOrderStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await orderStatusRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }


            var isColorExist = await orderStatusRepository.IsColorExistAsync(0, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<OrderStatus>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await orderStatusRepository.CreateAsync(newObj);
            await orderStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertOrderStatusDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await orderStatusRepository.FindByConditionAsync(c => c.Active);
            if (data==null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var temp = mapper.Map<List<OrderStatusDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await orderStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
            }
            var temp = mapper.Map<OrderStatusDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await orderStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await orderStatusRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateOrderStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await orderStatusRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }         
            
            var isColorExist = await orderStatusRepository.IsColorExistAsync(obj.Id, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await orderStatusRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await orderStatusRepository.UpdateAsync(updateObj);
            await orderStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateOrderStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
