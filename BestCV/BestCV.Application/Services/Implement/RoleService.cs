using AutoMapper;
using BestCV.Application.Models.Roles;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;
        public RoleService(IRoleRepository roleRepository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<RoleService>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: create new role
        /// </summary>
        /// <param name="obj">insert role DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertRoleDTO obj)
        {
            List<string> errors = new();
            var model = MappingInsertDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _roleRepository.CreateAsync(model);
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: insert list role
        /// </summary>
        /// <param name="objs">list role DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertRoleDTO> objs)
        {
            List<string> errors = new List<string>();
            var models = objs.Select(c => MappingInsertDTO(c));
            foreach(var item in models)
            {
                errors.AddRange(await Validate(item));
            }
            if (errors.Count>0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _roleRepository.CreateListAsync(models);
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Descripton: Get list all role
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _roleRepository.FindByConditionAsync(c=>c.Active);
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Get role by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await _roleRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound($"Not found role with id: {id}", id);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: soft delete role by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            await _roleRepository.SoftDeleteAsync(id);
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Soft delete list role
        /// </summary>
        /// <param name="objs">list role id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            foreach(var item in objs)
            {
                await _roleRepository.SoftDeleteAsync(item);
            }
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: update role
        /// </summary>
        /// <param name="obj">update role DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateRoleDTO obj)
        {
            List<string> errors = new();
            var model = await MappingUpdateRoleDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _roleRepository.UpdateAsync(model);
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: update list role
        /// </summary>
        /// <param name="obj">list role DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateRoleDTO> obj)
        {
            List<string> errors = new List<string>();
            List<Role> updateRoles = new List<Role>();
            foreach(var item in obj)
            {
                var updateRole = await MappingUpdateRoleDTO(item);
                errors.AddRange(await Validate(updateRole));
                updateRoles.Add(updateRole);
            }
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _roleRepository.UpdateListAsync(updateRoles);
            await _roleRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Auhthor: TUNGTD
        /// Created: 27/07/2023
        /// Description: Maaping insert role DTO to role
        /// </summary>
        /// <param name="obj">role DTO object</param>
        /// <returns></returns>
        public Role MappingInsertDTO(InsertRoleDTO obj)
        {
            Role role = _mapper.Map<Role>(obj);
            role.Id = 0;
            role.Active = true;
            role.CreatedTime = DateTime.Now;
            return role;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// </summary>
        /// <param name="dto">update role DTO object</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found role id</exception>
        public async Task<Role> MappingUpdateRoleDTO(UpdateRoleDTO dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.Id);
            if (role == null)
            {
                throw new Exception($"Not found role id:{dto.Id}");
            }
            role = _mapper.Map(dto, role);
            return role;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Creatd: 27/07/2023
        /// Description: Validate to role
        /// </summary>
        /// <param name="obj">role object</param>
        /// <returns></returns>
        public async Task<List<string>> Validate(Role obj)
        {
            List<string> errors = new List<string>();
            if (await _roleRepository.NameIsExisted(obj.Name, obj.Id)){
                errors.Add($"Tên vai trò đã tồn tại: {obj.Name}");
            }
            if (await _roleRepository.CODEIsExisted(obj.Code, obj.Id))
            {
                errors.Add($"Mã Code đã tồn tại: {obj.Code}");
            }
            return errors;
        }
    }
}
