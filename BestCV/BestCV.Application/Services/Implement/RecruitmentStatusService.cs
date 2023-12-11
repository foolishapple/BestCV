using AutoMapper;
using HandlebarsDotNet;
using BestCV.Application.Models.JobSkill;
using BestCV.Application.Models.PostStatus;
using BestCV.Application.Models.RecruitmentStatus;
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
    public class RecruitmentStatusService : IRecruitmentStatusService
    {
        private readonly IRecruitmentStatusRepository recruitmentStatusRepository;
        private readonly ILogger<IRecruitmentStatusService> logger;
        private readonly IMapper mapper;

        public RecruitmentStatusService(IRecruitmentStatusRepository recruitmentStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.recruitmentStatusRepository = recruitmentStatusRepository;
            this.logger = loggerFactory.CreateLogger<RecruitmentStatusService>();
            this.mapper = mapper;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add recruitment status
        /// </summary>
        /// <param name="obj">InsertRecruitmentStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertRecruitmentStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await recruitmentStatusRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }    
            
            var isColorExist = await recruitmentStatusRepository.IsColorExistAsync(0, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<RecruitmentStatus>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await recruitmentStatusRepository.CreateAsync(newObj);
            await recruitmentStatusRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertRecruitmentStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list recruitment status
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await recruitmentStatusRepository.FindByConditionAsync(s=>s.Active);
            if (data ==null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<RecruitmentStatusDTO>>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get recruitment status by id
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await recruitmentStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<RecruitmentStatusDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delte recruitment status by id
        /// </summary>
        /// <param name="id">RecruitmentStatusId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await recruitmentStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await recruitmentStatusRepository.SaveChangesAsync();
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
        /// Description: update recruitment status 
        /// </summary>
        /// <param name="obj">UpdateRecruitmentStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdateRecruitmentStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await recruitmentStatusRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }    
            
            var isColorExist = await recruitmentStatusRepository.IsColorExistAsync(obj.Id, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var data = await recruitmentStatusRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await recruitmentStatusRepository.UpdateAsync(updateObj);
            await recruitmentStatusRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateRecruitmentStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
