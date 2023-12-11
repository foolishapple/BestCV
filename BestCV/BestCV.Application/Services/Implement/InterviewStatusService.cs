using AutoMapper;
using BestCV.Application.Models.InterviewStatsus;
using BestCV.Application.Models.OrderStatus;
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
    public class InterviewStatusService : IInterviewStatusService
    {
        private readonly IInterviewStatusRepository interviewStatusRepository;
        private readonly ILogger<IInterviewStatusService> logger;
        private readonly IMapper mapper;

        public InterviewStatusService(IInterviewStatusRepository interviewStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.interviewStatusRepository = interviewStatusRepository;
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<InterviewStatusService>();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add interview status 
        /// </summary>
        /// <param name="obj">InsertInterviewStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertInterviewStatusDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await interviewStatusRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            var isColorExist = await interviewStatusRepository.IsColorExistAsync(0, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var jc = mapper.Map<InterviewStatus>(obj);
            jc.Id = 0;
            jc.Active = true;
            jc.CreatedTime = DateTime.Now;
            jc.Description = !string.IsNullOrEmpty(jc.Description) ?  jc.Description.ToEscape() : null;
            await interviewStatusRepository.CreateAsync(jc);
            await interviewStatusRepository.SaveChangesAsync();
            return DionResponse.Success(jc);

        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertInterviewStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list interview status
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await interviewStatusRepository.FindByConditionAsync(c=>c.Active);
            if (data==null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<InterviewStatusDTO>>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get detail interview status by id
        /// </summary>
        /// <param name="id">InterviewStausId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await interviewStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<InterviewStatusDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delete interview status by id
        /// </summary>
        /// <param name="id">InterviewStatusId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await interviewStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await interviewStatusRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            return DionResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: update interview status 
        /// </summary>
        /// <param name="obj">UpdateInterviewStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdateInterviewStatusDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await interviewStatusRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }    
            
            var isColorExist = await interviewStatusRepository.IsColorExistAsync(obj.Id, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var data = await interviewStatusRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await interviewStatusRepository.UpdateAsync(updateObj);
            await interviewStatusRepository.SaveChangesAsync();
            return DionResponse.Success(obj);


        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateInterviewStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
