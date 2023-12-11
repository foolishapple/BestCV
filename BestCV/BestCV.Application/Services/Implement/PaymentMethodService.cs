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

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add payment method
        /// </summary>
        /// <param name="obj">InsertPaymentMethodDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertPaymentMethodDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await paymentMethodRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PaymentMethod>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await paymentMethodRepository.CreateAsync(newObj);
            await paymentMethodRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);

        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPaymentMethodDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list payment method
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await paymentMethodRepository.FindByConditionAsync(c => c.Active);
            if (data==null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var temp = mapper.Map<List<PaymentMethodDTO>>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get payment method by id
        /// </summary>
        /// <param name="id">PaymentMethodId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await paymentMethodRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", id);
            }
            var temp = mapper.Map<PaymentMethodDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delete payment method by id
        /// </summary>
        /// <param name="id">PaymentMethodId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await paymentMethodRepository.SoftDeleteAsync(id);
            if (data)
            {
                await paymentMethodRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            return DionResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: update payment method 
        /// </summary>
        /// <param name="obj">UpdatePaymentMethodDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdatePaymentMethodDTO obj)
        {
            var listErrors = new List<string>();    
            var isNameExist = await paymentMethodRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var data = await paymentMethodRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await paymentMethodRepository.UpdateAsync(updateObj);
            await paymentMethodRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePaymentMethodDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
