using AutoMapper;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Models.TopJobUrgent;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TopJobUrgentService : ITopJobUrgentService
    {
        private readonly ITopJobUrgentRepository repository;
        private readonly ILogger<ITopJobUrgentService> logger;
        private readonly IMapper mapper;

        public TopJobUrgentService(ITopJobUrgentRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ITopJobUrgentService>();
            mapper = _mapper;
        }

        public async Task<DionResponse> ChangeOrderSort(ChangeOrderSortTopJobUrgentDTO model)
        {
            var orderSortUp = mapper.Map<TopJobUrgent>(model.SlideUp);
            var orderSortDown = mapper.Map<TopJobUrgent>(model.SlideDown);
            var listUpdate = new List<TopJobUrgent>();
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

        public async Task<DionResponse> CreateAsync(InsertTopJobUrgentDTO obj)
        {
            var data = mapper.Map<TopJobUrgent>(obj);
            var res = await repository.CheckOrderSort(0, obj.OrderSort);
            if (res)
            {
                var subOrderSort = await repository.MaxOrderSort(obj.OrderSort);
                data.SubOrderSort = subOrderSort + 1;
            }

            data.Active = true;
            data.CreatedTime = DateTime.Now;

            var error = await Validate(data);
            if (error.Count > 0)
            {
                return DionResponse.BadRequest(error);
            }
            await repository.CreateAsync(data);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTopJobUrgentDTO> objs)
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
            var result = mapper.Map<List<TopJobUrgentDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                throw new Exception($"Not found top job extra by id: {id}");
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> ListTopJobUrgent()
        {
            var data = await repository.ListTopJobUrgent();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> ListTopJobUrgentShowOnHomePageAsync()
        {
            var data = await repository.ListTopJobUrgentShowOnHomePageAsync();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SearchingUrgentJob(SearchJobWithServiceParameters parameter)
        {
            var data = await repository.SearchingUrgentJob(parameter);
            if(data.DataSource == null)
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

        public async Task<DionResponse> UpdateAsync(UpdateTopJobUrgentDTO obj)
        {
            var data = await repository.GetByIdAsync(obj.Id);


            if (data.OrderSort != obj.OrderSort)
            {
                var subOrderSort = await repository.MaxOrderSort(obj.OrderSort);
                obj.SubOrderSort = subOrderSort + 1;
            }

            if (data == null)
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
            return DionResponse.Success(obj); ;

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTopJobUrgentDTO> obj)
        {
            throw new NotImplementedException();
        }

        private async Task<List<string>> Validate(TopJobUrgent obj)
        {
            List<string> errors = new();
            if (await repository.IsJobIdExist(obj.Id, obj.JobId))
            {
                errors.Add($"Tên {obj.JobId} đã tồn tại.");
            }
            return errors;
        }
    }
}
