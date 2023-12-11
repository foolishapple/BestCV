using AutoMapper;
using BestCV.Application.Models.Coupon;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopJobExtra;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TopJobExtraService : ITopJobExtraService
    {
        private readonly ITopJobExtraRepository repository;
        private readonly ILogger<ITopJobExtraService> logger;
        private readonly IMapper mapper;

        public TopJobExtraService(ITopJobExtraRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ITopJobExtraService>();
            mapper = _mapper;
        }

        public async Task<DionResponse> ChangeOrderSort(ChangeOrderSortDTO model)
        {
            var orderSortUp = mapper.Map<TopJobExtra>(model.SlideUp);
            var orderSortDown = mapper.Map<TopJobExtra>(model.SlideDown);
            var listUpdate = new List<TopJobExtra>();
            var orderTemp = orderSortUp.OrderSort;
            orderSortUp.OrderSort = orderSortDown.OrderSort;
            orderSortDown.OrderSort = orderTemp;
            var subOrderTemp = orderSortUp.SubOrderSort;
            orderSortUp.SubOrderSort = orderSortDown.SubOrderSort;
            orderSortDown.SubOrderSort = subOrderTemp;
            listUpdate.Add(orderSortUp);
            listUpdate.Add(orderSortDown);
            await repository.ChangeOrderSort(listUpdate);
            await repository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        /// <summary>
        /// Author HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertTopJobExtraDTO obj)
        {
            var data = mapper.Map<TopJobExtra>(obj);
            var res = await repository.CheckOrderSort(0, obj.OrderSort);
            if (res)
            {
                var subOrderSort = await repository.MaxOrderSort(obj.OrderSort);
                data.SubOrderSort = subOrderSort + 1;
            }

            data.Active = true;
            data.CreatedTime = DateTime.Now;

            var error = await Validate(data);
            if(error.Count > 0)
            {
                return DionResponse.BadRequest(error); 
            }
            await repository.CreateAsync(data);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTopJobExtraDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TopJobExtraDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if(data == null)
            {
                throw new Exception($"Not found top job extra by id: {id}");
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> ListTopJobExtra()
        {
            var data = await repository.ListTopJobExtra();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var data = await repository.SoftDeleteAsync(id);
            if (data)
            {
                await repository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            throw new Exception($"Not found top job extra by id: {id}");
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateTopJobExtraDTO obj)
        {
            var data = await repository.GetByIdAsync(obj.Id);
            

            if(data.OrderSort != obj.OrderSort)
            {
                 var subOrderSort = await repository.MaxOrderSort(obj.OrderSort);
                 obj.SubOrderSort = subOrderSort + 1;
            }
           
            if(data == null)
            {
                throw new Exception($"Not found slide by id : {obj.Id}");
            }

            data = mapper.Map(obj, data);

            var error = await Validate(data);

            if (error.Count > 0)
            {
                return DionResponse.BadRequest(error);
            }
            await repository.UpdateAsync(data);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTopJobExtraDTO> obj)
        {
            throw new NotImplementedException();
        }
        private async Task<List<string>> Validate(TopJobExtra obj)
        {
            List<string> errors = new();
            if (await repository.IsJobIdExist(obj.Id, obj.JobId))
            {
                errors.Add($"Tên {obj.JobId} đã tồn tại.");
            }
            return errors;
        }

        public async Task<DionResponse> ListTopJobExtraShowOnHomePageAsync()
        {
            var data = await repository.ListTopJobExtraShowOnHomePageAsync();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter)
        {
            var data = await repository.SearchingFeatureJob(parameter);
            if(data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
    }
}
