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

        public async Task<BestCVResponse> CreateAsync(InsertCandidateApplyJobStatusDTO obj)
        {
            List<string> errors = new();
            var model = _mapper.Map<CandidateApplyJobStatus>(obj);
            model.Id = 0;
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateAsync(model);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateApplyJobStatusDTO> objs)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(models);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _repository.FindByConditionAsync(c => c.Active);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                throw new Exception($"Not found candidate job apply status with id:{id}");
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await _repository.SoftDeleteAsync(id);
            if (data)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu. ", id);
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
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
                return BestCVResponse.Success(objs);
            }
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateCandidateApplyJobStatusDTO obj)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(model);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateApplyJobStatusDTO> objs)
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
            return BestCVResponse.Success(objs);
        }

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
