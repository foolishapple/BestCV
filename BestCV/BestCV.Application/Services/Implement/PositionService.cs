using AutoMapper;
using BestCV.Application.Models.Position;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Core.Utilities;


namespace BestCV.Application.Services.Implement
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository positionRepository;
        private readonly ILogger<PositionService> logger;
        private readonly IMapper mapper;


        public PositionService(
            ILoggerFactory loggerFactory,
            IMapper _mapper,
            IPositionRepository _positionRepository
            )
        {
            positionRepository = _positionRepository;
            logger = loggerFactory.CreateLogger<PositionService>();
            mapper = _mapper;
        }
        /// <summary>
        /// Author : Thoai Anh
        /// CreatedTime : 31/08/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertPositionDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await positionRepository.IsNameExistAsync(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var model = mapper.Map<Position>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await positionRepository.CreateAsync(model);
            await positionRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPositionDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await positionRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<List<PositionDTO>>(data);

            return DionResponse.Success(model);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await positionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<PositionDTO>(data);
            return DionResponse.Success(model);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await positionRepository.SoftDeleteAsync(id);
            if (data)
            {
                await positionRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }

            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author : Thoai Anh 
        /// CreadTime : 31/08/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdatePositionDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await positionRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var position = await positionRepository.GetByIdAsync(obj.Id);
            if (position == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, position);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await positionRepository.UpdateAsync(model);
            await positionRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePositionDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
