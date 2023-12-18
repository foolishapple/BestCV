using AutoMapper;
using BestCV.Application.Models.EmployerNotification;
using BestCV.Application.Models.RecruitmentCampaigns;
using BestCV.Application.Services.Interfaces;
using BestCV.Application.Utilities.SignalRs.Hubs;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.RecruitmentCampaigns;
using BestCV.Domain.Constants;
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
    public class RecruitmentCampaignService : IRecruitmentCampaignService
    {
        private readonly IRecruitmentCampaignRepository _recruitmentCampaignRepository;
        private readonly IEmployerNotificationRepository _employerNotificationRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public RecruitmentCampaignService(IRecruitmentCampaignRepository recruitmentCampaignRepository, ILoggerFactory loggerFactory, IMapper mapper, IEmployerNotificationRepository employerNotificationRepository)
        {
            _recruitmentCampaignRepository = recruitmentCampaignRepository;
            _logger = loggerFactory.CreateLogger<RecruitmentCampaignService>();
            _mapper = mapper;
            _employerNotificationRepository = employerNotificationRepository;
        }

        public async Task<BestCVResponse> CreateAsync(InsertRecruitmentCampaignDTO obj)
        {
            List<string> errors = new(); 
            var model = _mapper.Map<RecruitmentCampaign>(obj);
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Search = "";
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _recruitmentCampaignRepository.CreateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertRecruitmentCampaignDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await _recruitmentCampaignRepository.GetByIdAsync(id);
            if (data != null)
            {
                return BestCVResponse.Success(data);
            }
            else
            {
                throw new Exception($"Not founde recruitment campaign by id: {id}");
            }
        }

        public async Task<BestCVResponse> ListToEmployer(long id)
        {
            var data = await _recruitmentCampaignRepository.FindByConditionAsync(c => c.EmployerId == id && c.Active);
            data = data.OrderByDescending(c => c.CreatedTime).ToList();
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateRecruitmentCampaignDTO obj)
        {
            List<string> errors = new();
            var model = await _recruitmentCampaignRepository.GetByIdAsync(obj.Id);
            if (model == null)
            {
                throw new Exception($"Not found recruiment with id: {obj.Id}");
            }
            model = _mapper.Map(obj, model);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _recruitmentCampaignRepository.UpdateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateRecruitmentCampaignDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters)
        {
            return await _recruitmentCampaignRepository.ListDTPaging(parameters);
        }

        public async Task<BestCVResponse> ChangeApproved(ChangeApproveRecruitmentCampaignDTO obj)
        {
            var model = await _recruitmentCampaignRepository.GetByIdAsync(obj.Id);
            if(model== null)
            {
                throw new Exception($"Not found recruiment with id: {obj.Id}");
            }
            model.IsAprroved = obj.IsApproved;
             await _recruitmentCampaignRepository.UpdateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<List<string>> Validate(RecruitmentCampaign obj)
        {
            List<string> errors = new();
            if(await _recruitmentCampaignRepository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add("Tên chiến dịch đã tồn tại.");
            }
            return errors;
        }

        public async Task<BestCVResponse> ListOpenedByEmployer(long id)
        {
            var data = await _recruitmentCampaignRepository.FindByConditionAsync(c => c.Active && c.IsAprroved && c.EmployerId == id);
            return BestCVResponse.Success(data);
        }
    }
}
