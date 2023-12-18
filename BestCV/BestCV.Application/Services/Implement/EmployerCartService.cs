using AutoMapper;
using BestCV.Application.Models.EmployerCarts;
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
    public class EmployerCartService : IEmployerCartService
    {
        private readonly IEmployerCartRepository repository;
        private readonly ILogger<IEmployerCartService> logger;
        private readonly IMapper mapper;
        public EmployerCartService(IEmployerCartRepository repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            this.repository = repository;
            logger = loggerFactory.CreateLogger<IEmployerCartService>();
            mapper = _mapper;
        }

        public async Task<BestCVResponse> AddToCart(int servicePackageId, long employerId)
        {
            var newCart = new EmployerCart()
            {
                Active = true,
                CreatedTime = DateTime.Now,
                EmployerId = employerId,
                Quantity = 0,
                EmployerServicePackageId = servicePackageId,
            };
            var data = await repository.FindByConditionAsync(x => x.Active && x.EmployerServicePackageId == servicePackageId && x.EmployerId == employerId);
            if(data.Count > 0)
            {
                newCart.Quantity = data[0].Quantity + 1;
                newCart.Id = data[0].Id;
                await repository.UpdateAsync(newCart);
            }
            else
            {
                await repository.CreateAsync(newCart);
            }
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(data);
        }

        public async Task<int> CountServicePackageInCart(long employerId)
        {
            var data = await repository.CountByConditionAsync(x=>x.Active && x.EmployerId == employerId);
            return data;
        }

        public async Task<BestCVResponse> CreateAsync(InsertEmployerCartDTO obj)
        {
            var data = mapper.Map<EmployerCart>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            await repository.CreateAsync(data);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerCartDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x=>x.Active);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerCartDTO>>(data);
            return BestCVResponse.Success(result);    
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<EmployerCartDTO>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> ListByEmployerId(long employerId)
        {
            var data = await repository.ListByEmployerId(employerId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var result = await repository.SoftDeleteAsync(id);
            if (result)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(id);
            }
            return BestCVResponse.BadRequest(id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateEmployerCartDTO obj)
        {
            var data = await repository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var update = mapper.Map(obj, data);
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            
            await repository.UpdateAsync(update);

            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerCartDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
