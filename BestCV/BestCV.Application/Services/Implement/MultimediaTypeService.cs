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


        public async Task<BestCVResponse> CreateAsync(InsertMultimediaTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await multimediaTypeRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<MultimediaType>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await multimediaTypeRepository.CreateAsync(newObj);
            await multimediaTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);

        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertMultimediaTypeDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await multimediaTypeRepository.FindByConditionAsync(c => c.Active);
            if (data==null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<MultimediaTypeDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await multimediaTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<MultimediaTypeDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await multimediaTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await multimediaTypeRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

 
        public async Task<BestCVResponse> UpdateAsync(UpdateMultimediaTypeDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await multimediaTypeRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await multimediaTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;
            await multimediaTypeRepository.UpdateAsync(updateObj);
            await multimediaTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateMultimediaTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
