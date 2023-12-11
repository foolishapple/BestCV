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

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> CreateAsync(InsertTopFeatureJobDTO obj)
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
                return DionResponse.BadRequest(error);
            }

            topFeatureJob.Active = true;
            topFeatureJob.CreatedTime = DateTime.Now;

            await topFeatureJobRepository.CreateAsync(topFeatureJob);
            await topFeatureJobRepository.SaveChangesAsync();

            return DionResponse.Success(topFeatureJob);
        }


        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTopFeatureJobDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await topFeatureJobRepository.FindByConditionAsync(c => c.Active && c.Job.Active);

            if (data == null || data.Count == 0)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TopFeatureJobDTO>>(data);
            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await topFeatureJobRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
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

            return DionResponse.Success(result);
        }


        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await topFeatureJobRepository.ListJobSelected();
        }

        //public Task<DionResponse> ListTopFeatureJobShowOnHomePageAsync()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<DionResponse> searchJobs(Select2Aggregates select2Aggregates)
        {
            if (select2Aggregates != null
                && !string.IsNullOrEmpty(select2Aggregates.SearchString)
                && select2Aggregates.PageLimit.HasValue
                && select2Aggregates.PageLimit > 0)
            {
                var result = await topFeatureJobRepository.searchJobs(select2Aggregates);
                return DionResponse.Success(result);
            }
            else
            {
                return DionResponse.BadRequest("Không có dữ liệu.");
            }
        }


        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await topFeatureJobRepository.SoftDeleteAsync(id);
            if (data)
            {
                await topFeatureJobRepository.SaveChangesAsync();
                return DionResponse.Success();
            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 16/8/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> UpdateAsync(UpdateTopFeatureJobDTO obj)
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
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }

            await topFeatureJobRepository.UpdateAsync(topFeatureJob);
            await topFeatureJobRepository.SaveChangesAsync();

            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTopFeatureJobDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> ListTopFeatureJobShowOnHomePageAsync()
        {
            var data = await topFeatureJobRepository.ListTopFeatureJobShowOnHomePageAsync();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter)
        {
            var data = await topFeatureJobRepository.SearchingFeatureJob(parameter);
            if (data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 09/10/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DionResponse> ChangeOrderSort(ChangeTopFeatureJobDTO model)
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
            return DionResponse.Success(model);
        }
        public async Task<DionResponse> ListFeatureJob()
        {
            var data = await topFeatureJobRepository.ListTopFeatureJob();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
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
