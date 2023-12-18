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

        public async Task<BestCVResponse> CreateAsync(InsertPositionDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await positionRepository.IsNameExistAsync(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var model = mapper.Map<Position>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await positionRepository.CreateAsync(model);
            await positionRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPositionDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await positionRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<List<PositionDTO>>(data);

            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await positionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<PositionDTO>(data);
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await positionRepository.SoftDeleteAsync(id);
            if (data)
            {
                await positionRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }

            return BestCVResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
 
        public async Task<BestCVResponse> UpdateAsync(UpdatePositionDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await positionRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var position = await positionRepository.GetByIdAsync(obj.Id);
            if (position == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, position);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await positionRepository.UpdateAsync(model);
            await positionRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePositionDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
