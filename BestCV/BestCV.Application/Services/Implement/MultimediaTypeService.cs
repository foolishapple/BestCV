using AutoMapper;
using BestCV.Application.Models.MultimediaType;
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
    public class MultimediaTypeService : IMultimediaTypeService
    {
        private readonly IMultimediaTypeRepository multimediaTypeRepository;
        private readonly ILogger<IMultimediaTypeService> logger;
        private readonly IMapper mapper;

        public MultimediaTypeService(IMultimediaTypeRepository multimediaTypeRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.multimediaTypeRepository = multimediaTypeRepository;
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<MultimediaTypeService>();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add multimedia type 
        /// </summary>
        /// <param name="obj">InsertMultimediaTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertMultimediaTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await multimediaTypeRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<MultimediaType>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await multimediaTypeRepository.CreateAsync(newObj);
            await multimediaTypeRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);

        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertMultimediaTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list multimedia type 
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await multimediaTypeRepository.FindByConditionAsync(c => c.Active);
            if (data==null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<MultimediaTypeDTO>>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get multimedia type by id 
        /// </summary>
        /// <param name="id">MultimediaTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await multimediaTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<MultimediaTypeDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delete multimedia type by id
        /// </summary>
        /// <param name="id">MultimediaTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await multimediaTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await multimediaTypeRepository.SaveChangesAsync();
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
        /// Description: update multimedia type
        /// </summary>
        /// <param name="obj">UpdateMultimediaTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdateMultimediaTypeDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await multimediaTypeRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var data = await multimediaTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;
            await multimediaTypeRepository.UpdateAsync(updateObj);
            await multimediaTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateMultimediaTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
