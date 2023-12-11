using AutoMapper;
using BestCV.Application.Models.JobType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly IMapper mapper;
        public JobTypeService(IJobTypeRepository _jobTypeRepository, IMapper _mapper)
        {
            jobTypeRepository = _jobTypeRepository;
            mapper = _mapper;
        }
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : Insert JobType
        /// </summary>
        /// <param name="obj"> DTO JobType</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertJobTypeDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await jobTypeRepository.IsNameExistAsync(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var model = mapper.Map<JobType>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await jobTypeRepository.CreateAsync(model);
            await jobTypeRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertJobTypeDTO> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : Get list JobType 
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await jobTypeRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<List<JobTypeDTO>>(data);
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : Get JobType by Id
        /// </summary>
        /// <param name="id"> JobType Id</param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {

            var data = await jobTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<JobTypeDTO>(data);
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : Soft delete obType by Id
        /// </summary>
        /// <param name="id">JobType Id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await jobTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await jobTypeRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            
            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : Update JobType by update JobTypeDTO
        /// </summary>
        /// <param name="obj">UpdateJobTypeDTO</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateJobTypeDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await jobTypeRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var jobType = await jobTypeRepository.GetByIdAsync(obj.Id);
            if (jobType == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, jobType);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await jobTypeRepository.UpdateAsync(model);
            await jobTypeRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
