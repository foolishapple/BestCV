using AutoMapper;
using BestCV.Application.Models.RecruitmentCampaignStatuses;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class RecruitmentCampaignStatusService : IRecruitmentCampaignStatusService
    {
        private readonly IRecruitmentCampaignStatusRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public RecruitmentCampaignStatusService(IRecruitmentCampaignStatusRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<RecruitmentCampaignStatusService>();
            _mapper = mapper;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new Recruitment Campaign Status
        /// </summary>
        /// <param name="obj">Recruitment Campaign Status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertRecruitmentCampaignStatusDTO obj)
        {
            var item = _mapper.Map<RecruitmentCampaignStatus>(obj);
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateAsync(item);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new list Recruitment Campaign Status
        /// </summary>
        /// <param name="objs">list Recruitment Campaign Status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertRecruitmentCampaignStatusDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<RecruitmentCampaignStatus>(c));
            foreach (var item in items)
            {
                item.Active = true;
                item.CreatedTime = DateTime.Now;
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(items);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get list all Recruitment Campaign Status
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _repository.FindByConditionAsync(c => c.Active);
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get Recruitment Campaign Status by id
        /// </summary>
        /// <param name="id">Recruitment Campaign Status id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found Recruitment Campaign Status by id: {id}");
            }
            return DionResponse.Success(item);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete Recruitment Campaign Status by id
        /// </summary>
        /// <param name="id">Recruitment Campaign Status by id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var result = await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success(id);
            }
            throw new Exception($"Not found Recruitment Campaign Status by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete list Recruitment Campaign Status by list Recruitment Campaign Status id
        /// </summary>
        /// <param name="objs">list Recruitment Campaign Status id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Failed delete</exception>
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            var result = await _repository.SoftDeleteListAsync(objs);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success(objs);
            }
            throw new Exception($"Failed to soft delete list Recruitment Campaign Status by list Recruitment Campaign Status id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Update Recruitment Campaign Status
        /// </summary>
        /// <param name="obj">update Recruitment Campaign Status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateRecruitmentCampaignStatusDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found Recruitment Campaign Status by id: {obj.Id}");
            }
            item = _mapper.Map(obj, item);
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: update list Recruitment Campaign Status
        /// </summary>
        /// <param name="obj">list Recruitment Campaign Status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateRecruitmentCampaignStatusDTO> obj)
        {
            List<RecruitmentCampaignStatus> updateItems = new();
            List<string> errors = new();
            foreach (var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found Recruitment Campaign Status by id: {item.Id}");
                }
                model = _mapper.Map(item, model);
                updateItems.Add(model);
                errors.AddRange(await Validate(model));
            }
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateListAsync(updateItems);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Validate to Recruitment Campaign Status
        /// </summary>
        /// <param name="obj">Recruitment Campaign Status object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(RecruitmentCampaignStatus obj)
        {
            List<string> errors = new();
            if (await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            if (await _repository.ColorIsExisted(obj.Color, obj.Id))
            {
                errors.Add($"Màu {obj.Name} đã tồn tại.");
            }
            return errors;
        }
    }
}
