using AutoMapper;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PaymentMethod;
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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ILogger<IPaymentMethodService> logger;
        private readonly IMapper mapper;
        private readonly IPaymentMethodRepository paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.paymentMethodRepository = paymentMethodRepository;
            this.logger = loggerFactory.CreateLogger<PaymentMethodService>();
        }


        public async Task<BestCVResponse> CreateAsync(InsertPaymentMethodDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await paymentMethodRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PaymentMethod>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await paymentMethodRepository.CreateAsync(newObj);
            await paymentMethodRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);

        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPaymentMethodDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await paymentMethodRepository.FindByConditionAsync(c => c.Active);
            if (data==null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var temp = mapper.Map<List<PaymentMethodDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await paymentMethodRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);
            }
            var temp = mapper.Map<PaymentMethodDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await paymentMethodRepository.SoftDeleteAsync(id);
            if (data)
            {
                await paymentMethodRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdatePaymentMethodDTO obj)
        {
            var listErrors = new List<string>();    
            var isNameExist = await paymentMethodRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var data = await paymentMethodRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await paymentMethodRepository.UpdateAsync(updateObj);
            await paymentMethodRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePaymentMethodDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
