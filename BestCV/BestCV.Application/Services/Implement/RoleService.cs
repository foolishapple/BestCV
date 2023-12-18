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

        public async Task<BestCVResponse> CreateAsync(InsertRoleDTO obj)
        {
            List<string> errors = new();
            var model = MappingInsertDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _roleRepository.CreateAsync(model);
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
  
        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertRoleDTO> objs)
        {
            List<string> errors = new List<string>();
            var models = objs.Select(c => MappingInsertDTO(c));
            foreach(var item in models)
            {
                errors.AddRange(await Validate(item));
            }
            if (errors.Count>0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _roleRepository.CreateListAsync(models);
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _roleRepository.FindByConditionAsync(c=>c.Active);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _roleRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound($"Not found role with id: {id}", id);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            await _roleRepository.SoftDeleteAsync(id);
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(id);
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            foreach(var item in objs)
            {
                await _roleRepository.SoftDeleteAsync(item);
            }
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateRoleDTO obj)
        {
            List<string> errors = new();
            var model = await MappingUpdateRoleDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _roleRepository.UpdateAsync(model);
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateRoleDTO> obj)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _roleRepository.UpdateListAsync(updateRoles);
            await _roleRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Role MappingInsertDTO(InsertRoleDTO obj)
        {
            Role role = _mapper.Map<Role>(obj);
            role.Id = 0;
            role.Active = true;
            role.CreatedTime = DateTime.Now;
            return role;
        }

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
