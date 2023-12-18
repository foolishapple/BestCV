using AutoMapper;

using BestCV.Application.Models.TopFeatureJob;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Job;

using BestCV.Domain.Aggregates.TopFeatureJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TopFeatureJobService : ITopFeatureJobService
    {
        private readonly ITopFeatureJobRepository topFeatureJobRepository;
        private readonly ILogger<ITopFeatureJobService> logger;
        private readonly IMapper mapper;

        public TopFeatureJobService(ITopFeatureJobRepository _topFeatureJobRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            topFeatureJobRepository = _topFeatureJobRepository;
            logger = loggerFactory.CreateLogger<ITopFeatureJobService>();
            mapper = _mapper;
        }


        public async Task<BestCVResponse> CreateAsync(InsertTopFeatureJobDTO obj)
        {
          
           
            var topFeatureJob = mapper.Map<TopFeatureJob>(obj);
            var res = await topFeatureJobRepository.CheckOrderSort(0, obj.OrderSort);
            if (res)
            {
                var subOrderSort = await topFeatureJobRepository.MaxOrderSort(obj.OrderSort);
                topFeatureJob.SubOrderSort = subOrderSort + 1;
            }
            var error = await Validate(topFeatureJob);
            if (error.Count > 0)
            {
                return BestCVResponse.BadRequest(error);
            }

            topFeatureJob.Active = true;
            topFeatureJob.CreatedTime = DateTime.Now;

            await topFeatureJobRepository.CreateAsync(topFeatureJob);
            await topFeatureJobRepository.SaveChangesAsync();

            return BestCVResponse.Success(topFeatureJob);
        }


        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertTopFeatureJobDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await topFeatureJobRepository.FindByConditionAsync(c => c.Active && c.Job.Active);

            if (data == null || data.Count == 0)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TopFeatureJobDTO>>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await topFeatureJobRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }

            var result = new TopFeatureJobDTO
            {
                Id = data.Id,
                JobId = data.JobId,
                JobName = data.Job.Name,
                OrderSort = data.OrderSort,
                Active = data.Active,
                CreatedTime = DateTime.Now
            };

            return BestCVResponse.Success(result);
        }



        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await topFeatureJobRepository.ListJobSelected();
        }

        //public Task<BestCVResponse> ListTopFeatureJobShowOnHomePageAsync()
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<BestCVResponse> searchJobs(Select2Aggregates select2Aggregates)
        {
            if (select2Aggregates != null
                && !string.IsNullOrEmpty(select2Aggregates.SearchString)
                && select2Aggregates.PageLimit.HasValue
                && select2Aggregates.PageLimit > 0)
            {
                var result = await topFeatureJobRepository.searchJobs(select2Aggregates);
                return BestCVResponse.Success(result);
            }
            else
            {
                return BestCVResponse.BadRequest("Không có dữ liệu.");
            }
        }



        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await topFeatureJobRepository.SoftDeleteAsync(id);
            if (data)
            {
                await topFeatureJobRepository.SaveChangesAsync();
                return BestCVResponse.Success();
            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateTopFeatureJobDTO obj)
        {
            var topFeatureJob = await topFeatureJobRepository.GetByIdAsync(obj.Id);
            if (topFeatureJob.OrderSort != obj.OrderSort)
            {
                var subOrderSort = await topFeatureJobRepository.MaxOrderSort(obj.OrderSort);
                obj.SubOrderSort = subOrderSort + 1;
            }
            topFeatureJob = mapper.Map(obj, topFeatureJob);
            if (topFeatureJob == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }

            await topFeatureJobRepository.UpdateAsync(topFeatureJob);
            await topFeatureJobRepository.SaveChangesAsync();

            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateTopFeatureJobDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> ListTopFeatureJobShowOnHomePageAsync()
        {
            var data = await topFeatureJobRepository.ListTopFeatureJobShowOnHomePageAsync();
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter)
        {
            var data = await topFeatureJobRepository.SearchingFeatureJob(parameter);
            if (data.DataSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> ChangeOrderSort(ChangeTopFeatureJobDTO model)
        {
            var orderSortUp = mapper.Map<TopFeatureJob>(model.SlideUp);
            var orderSortDown = mapper.Map<TopFeatureJob>(model.SlideDown);
            var listUpdate = new List<TopFeatureJob>();
            var orderTemp = orderSortUp.OrderSort;
            orderSortUp.OrderSort = orderSortDown.OrderSort;
            orderSortDown.OrderSort = orderTemp;
            var subOrderTemp = orderSortUp.SubOrderSort;
            orderSortUp.SubOrderSort = orderSortDown.SubOrderSort;
            orderSortDown.SubOrderSort = subOrderTemp;
            listUpdate.Add(orderSortUp);
            listUpdate.Add(orderSortDown);
            await topFeatureJobRepository.ChangeOrderSort(listUpdate);
            await topFeatureJobRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
        public async Task<BestCVResponse> ListFeatureJob()
        {
            var data = await topFeatureJobRepository.ListTopFeatureJob();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
        private async Task<List<string>> Validate(TopFeatureJob obj)
        {
            List<string> errors = new();
            if (await topFeatureJobRepository.IsFeatureIdExist(obj.Id, obj.JobId))
            {
                errors.Add($"Tên công việc đã tồn tại.");
            }
            return errors;
        }
    }
}
