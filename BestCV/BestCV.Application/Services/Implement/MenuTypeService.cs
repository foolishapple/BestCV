using AutoMapper;
using BestCV.Application.Models.Menu;
using BestCV.Application.Models.MenuType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class MenuTypeService : IMenuTypeService
    {
        private readonly IMenuTypeRepository menuTypeRepository;
        private readonly ILogger<IMenuTypeService> logger;
        private readonly IMapper mapper;

        public MenuTypeService(IMenuTypeRepository _menuTypeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            menuTypeRepository = _menuTypeRepository;
            logger = loggerFactory.CreateLogger<IMenuTypeService>();
            mapper = _mapper;
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 31/08/2023
        /// Description : Add menu type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertMenuTypeDTO obj)
        {
            var data = mapper.Map<MenuType>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;

            var error = await Validate(data);
            if(error.Count > 0)
            {
                return DionResponse.BadRequest(error);
            }

            await menuTypeRepository.CreateAsync(data);
            await menuTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertMenuTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 31/08/2023
        /// Description : Add menu type
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await menuTypeRepository.FindByConditionAsync(x => x.Active);
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }

            var res = mapper.Map<List<MenuTypeDTO>>(data);
            return DionResponse.Success(res);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await menuTypeRepository.GetByIdAsync(id);
            if(data == null)
            {
                throw new Exception($"Not found menu type by id: {id}");
            }

            return DionResponse.Success(data);
        }

        public async Task<DionResponse> GetMenuTypeAsync()
        {
            var data = await menuTypeRepository.FindByConditionAsync(x => x.Active && x.Id != 1001 );
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }

            var res = mapper.Map<List<MenuTypeDTO>>(data);
            return DionResponse.Success(res);
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 31/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await menuTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await menuTypeRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            throw new Exception($"Not found menu type by id: {id}");
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 31/08/2023
        /// Description : Update menu type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateMenuTypeDTO obj)
        {
            var data = await menuTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                throw new Exception($"Not found menu type by id: {obj.Id}");
            }

            data = mapper.Map(obj, data);
            var error = await Validate(data);
            if (error.Count > 0)
            {
                return DionResponse.BadRequest(error);
            }
            await menuTypeRepository.UpdateAsync(data);
            await menuTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateMenuTypeDTO> obj)
        {
            throw new NotImplementedException();
        }

        private async Task<List<string>> Validate(MenuType obj)
        {
            List<string> errors = new();
            if (await menuTypeRepository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            return errors;
        }
    }
}
