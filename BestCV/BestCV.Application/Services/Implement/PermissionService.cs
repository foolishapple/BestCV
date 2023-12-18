using AutoMapper;
using BestCV.Application.Models.Permissions;
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
    internal class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;
        public PermissionService(IPermissionRepository PermissionRepository, IMapper mapper, ILoggerFactory loggerFactory, IRolePermissionRepository rolePermissionRepository)
        {
            _permissionRepository = PermissionRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<PermissionService>();
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<BestCVResponse> CreateAsync(InsertPermissionDTO obj)
        {
            List<string> errors = new();
            var model = MappingInsertDTO(obj);
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            using (var trans = await _permissionRepository.BeginTransactionAsync())
            {
                try
                {
                    await _permissionRepository.CreateAsync(model);
                    await _permissionRepository.SaveChangesAsync();
                    if (obj.Roles.Count > 0)
                    {
                        var roles = obj.Roles.Select(c => new RolePermission()
                        {
                            Active = true,
                            PermissionId = model.Id,
                            CreatedTime = DateTime.Now,
                            RoleId = c
                        });
                        await _rolePermissionRepository.CreateListAsync(roles);
                        await _rolePermissionRepository.SaveChangesAsync();
                    }
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    throw new Exception("Failed to create new permission");
                }
            }
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPermissionDTO> objs)
        {
            List<string> errors = new List<string>();
            var models = objs.Select(c => MappingInsertDTO(c));
            foreach (var item in models)
            {
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _permissionRepository.CreateListAsync(models);
            await _permissionRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _permissionRepository.FindByConditionAsync(c=>c.Active);
            return BestCVResponse.Success(data);
        }
  
        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _permissionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound($"Not found Permission with id: {id}", id);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            await _permissionRepository.SoftDeleteAsync(id);
            await _permissionRepository.SaveChangesAsync();
            return BestCVResponse.Success(id);
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            foreach (var item in objs)
            {
                await _permissionRepository.SoftDeleteAsync(item);
            }
            await _permissionRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> UpdateAsync(UpdatePermissionDTO obj)
        {
            List<string> errors = new();
            var model = await MappingUpdatePermissionDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            using (var trans = await _rolePermissionRepository.BeginTransactionAsync())
            {
                try
                {
                    await _permissionRepository.UpdateAsync(model);
                    await _permissionRepository.SaveChangesAsync();
                    var roles = await _rolePermissionRepository.FindByConditionAsync(c => c.Active && c.PermissionId == model.Id);
                    IEnumerable<int> listDelete = roles.Where(c => !obj.Roles.Contains(c.RoleId)).Select(c => c.Id);
                    List<RolePermission> listAdd = obj.Roles.Select(c => new RolePermission()
                    {
                        Active = true,
                        PermissionId = model.Id,
                        CreatedTime = DateTime.Now,
                        RoleId = c
                    }).Where(c => !roles.Select(g => g.RoleId).Contains(c.RoleId)).ToList();
                    if (listDelete.Count() > 0)
                    {
                        await _rolePermissionRepository.HardDeleteListAsync(listDelete);
                    }
                    if (listAdd.Count() > 0)
                    {
                        await _rolePermissionRepository.CreateListAsync(listAdd);
                    }
                    await _rolePermissionRepository.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw new Exception();
                }
            }
            
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePermissionDTO> obj)
        {
            List<string> errors = new List<string>();
            List<Permission> updatePermissions = new List<Permission>();
            foreach (var item in obj)
            {
                var updatePermission = await MappingUpdatePermissionDTO(item);
                errors.AddRange(await Validate(updatePermission));
                updatePermissions.Add(updatePermission);
            }
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _permissionRepository.UpdateListAsync(updatePermissions);
            await _permissionRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Permission MappingInsertDTO(InsertPermissionDTO obj)
        {
            Permission Permission = _mapper.Map<Permission>(obj);
            Permission.Id = 0;
            Permission.Active = true;
            Permission.CreatedTime = DateTime.Now;
            return Permission;
        }

        public async Task<Permission> MappingUpdatePermissionDTO(UpdatePermissionDTO dto)
        {
            var Permission = await _permissionRepository.GetByIdAsync(dto.Id);
            if (Permission == null)
            {
                throw new Exception($"Not found Permission id:{dto.Id}");
            }
            Permission = _mapper.Map(dto, Permission);
            return Permission;
        }

        public async Task<List<string>> Validate(Permission obj)
        {
            List<string> errors = new List<string>();
            if (await _permissionRepository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên quyền đã tồn tại: {obj.Name}");
            }
            if (await _permissionRepository.CODEIsExisted(obj.Code, obj.Id))
            {
                errors.Add($"Mã Code đã tồn tại: {obj.Code}");
            }
            return errors;
        }

        public async Task<BestCVResponse> Detail(int id)
        {
            var data = await _permissionRepository.Detail(id);
            return BestCVResponse.Success(data);
        }
    }
}
