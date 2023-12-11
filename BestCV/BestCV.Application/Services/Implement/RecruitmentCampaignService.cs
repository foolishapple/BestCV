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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: created new recruit
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertRecruitmentCampaignDTO obj)
        {
            List<string> errors = new(); 
            var model = _mapper.Map<RecruitmentCampaign>(obj);
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            model.Search = "";
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _recruitmentCampaignRepository.CreateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertRecruitmentCampaignDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/008/2023
        /// Description: Get recruitment campaign by id
        /// </summary>
        /// <param name="id">recruitment campaign id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found exception</exception>
        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await _recruitmentCampaignRepository.GetByIdAsync(id);
            if (data != null)
            {
                return DionResponse.Success(data);
            }
            else
            {
                throw new Exception($"Not founde recruitment campaign by id: {id}");
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 16/08/2023
        /// Description: List Recruitment Campaign to employer
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        public async Task<DionResponse> ListToEmployer(long id)
        {
            var data = await _recruitmentCampaignRepository.FindByConditionAsync(c => c.EmployerId == id && c.Active);
            data = data.OrderByDescending(c => c.CreatedTime).ToList();
            return DionResponse.Success(data);
        }

        public Task<DionResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Creaed: 22/08/2023
        /// Description: update recruitment campain
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateRecruitmentCampaignDTO obj)
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
                return DionResponse.BadRequest(errors);
            }
            await _recruitmentCampaignRepository.UpdateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateRecruitmentCampaignDTO> obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 21/08/2023
        /// Description: list Recruitment Campaign aggregate datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DTResult<RecruitmentCampaignAggregate>> ListDTPaging(DTRecruitmentCampaignParameter parameters)
        {
            return await _recruitmentCampaignRepository.ListDTPaging(parameters);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Creaed: 22/08/2023
        /// Description: change approved to recruitment campain
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> ChangeApproved(ChangeApproveRecruitmentCampaignDTO obj)
        {
            var model = await _recruitmentCampaignRepository.GetByIdAsync(obj.Id);
            if(model== null)
            {
                throw new Exception($"Not found recruiment with id: {obj.Id}");
            }
            model.IsAprroved = obj.IsApproved;
             await _recruitmentCampaignRepository.UpdateAsync(model);
            await _recruitmentCampaignRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: validation for recruitment campagin
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<List<string>> Validate(RecruitmentCampaign obj)
        {
            List<string> errors = new();
            if(await _recruitmentCampaignRepository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add("Tên chiến dịch đã tồn tại.");
            }
            return errors;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: List recruitment campaign opened to employer
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        public async Task<DionResponse> ListOpenedByEmployer(long id)
        {
            var data = await _recruitmentCampaignRepository.FindByConditionAsync(c => c.Active && c.IsAprroved && c.EmployerId == id);
            return DionResponse.Success(data);
        }
    }
}
