using AutoMapper;
using BestCV.Application.Models.CandidateApplyJobStatuses;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class CandidateApplyJobStatusService : ICandidateApplyJobStatusService
    {
        private readonly ICandidateApplyJobStatusRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CandidateApplyJobStatusService(ICandidateApplyJobStatusRepository repository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CandidateApplyJobStatusService>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: create new candidate apply job status
        /// </summary>
        /// <param name="obj">insert candidate apply job status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertCandidateApplyJobStatusDTO obj)
        {
            List<string> errors = new();
            var model = _mapper.Map<CandidateApplyJobStatus>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateAsync(model);
            await _repository.SaveChangesAsync();
            return DionResponse.Success();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: create new list candidate apply job status
        /// </summary>
        /// <param name="objs">list candidate apply job status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertCandidateApplyJobStatusDTO> objs)
        {
            List<string> errors = new();
            var models = objs.Select(c => _mapper.Map<CandidateApplyJobStatus>(c));
            foreach (var item  in models)
            {
                item.Id = 0;
                item.Active = true;
                item.CreatedTime = DateTime.Now;
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(models);
            await _repository.SaveChangesAsync();
            return DionResponse.Success();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: get list all candidate apply job status
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _repository.FindByConditionAsync(c => c.Active);
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: get candidate apply job status by id
        /// </summary>
        /// <param name="id">candidate apply job status id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found candidate apply job status</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                throw new Exception($"Not found candidate job apply status with id:{id}");
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Soft delete candidate apply job status by id
        /// </summary>
        /// <param name="id">candidate apply job status id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Failed to soft delete</exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await _repository.SoftDeleteAsync(id);
            if (data)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success();

            }
            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: Soft delet list candidate apply job status by list id
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            using(var trans = await _repository.BeginTransactionAsync())
            {
                var result = await _repository.SoftDeleteListAsync(objs);
                if (!result)
                {
                    await trans.RollbackAsync();
                    throw new Exception("Failed to soft delete list candidate job apply status");
                }
                await trans.CommitAsync();
                return DionResponse.Success(objs);
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: update candidate apply job status
        /// </summary>
        /// <param name="obj">candidate apply job status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateCandidateApplyJobStatusDTO obj)
        {
            List<string> errors = new();
            var data = await _repository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                throw new Exception($"Not found candidate job apply status with id:{obj.Id}");
            }
            var model = _mapper.Map(obj, data);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(model);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: update list candidate apply job status
        /// </summary>
        /// <param name="objs">List candidate apply job status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCandidateApplyJobStatusDTO> objs)
        {
            List<CandidateApplyJobStatus> updates = new();
            foreach(var item in objs)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found candidate job apply status with id:{item.Id}");
                }
                model = _mapper.Map(item, model);
                updates.Add(model);
            }
            await _repository.UpdateListAsync(updates);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 04/08/2023
        /// Description: validate to candidate apply job status
        /// </summary>
        /// <param name="obj">candidate apply job status object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(CandidateApplyJobStatus obj)
        {
            List<string> errors = new();
            if(await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            if(await _repository.ColorIsExisted(obj.Color, obj.Id))
            {
                errors.Add($"Màu {obj.Color} đã tồn tại.");
            }
            return errors;
        }
    }
}
