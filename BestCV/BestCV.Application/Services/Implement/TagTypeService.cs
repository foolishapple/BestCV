using AutoMapper;
using BestCV.Application.Models.TagType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class TagTypeService : ITagTypeService
    { 
        private readonly ITagtypeRepository tagtypeRepository;
        private readonly ILogger<ITagTypeService> logger;
        private readonly IMapper mapper;
        public TagTypeService(ITagtypeRepository _tagtypeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            tagtypeRepository = _tagtypeRepository;
            logger = loggerFactory.CreateLogger<TagTypeService>();
            mapper = _mapper;
        }
        public async Task<DionResponse> CreateAsync(InsertTagTypeDTO obj)
        {
            var data = mapper.Map<TagType>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await tagtypeRepository.IsTagTypeExistAsync(data.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await tagtypeRepository.CreateAsync(data);
            await tagtypeRepository.SaveChangesAsync();
            return DionResponse.Success(data);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTagTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await tagtypeRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TagType>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await tagtypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<TagTypeDTO>(data);

            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await tagtypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return DionResponse.Success();
                await tagtypeRepository.SaveChangesAsync();
                return DionResponse.Success();

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateTagTypeDTO obj)
        {
            var data = await tagtypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var result = mapper.Map(obj, data);
            result.Description = !string.IsNullOrEmpty(result.Description) ? result.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await tagtypeRepository.IsTagTypeExistAsync(result.Name, result.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await tagtypeRepository.UpdateAsync(result);
            await tagtypeRepository.SaveChangesAsync();
            return DionResponse.Success(result);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTagTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
