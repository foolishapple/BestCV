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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: create new Permission
        /// </summary>
        /// <param name="obj">insert Permission DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertPermissionDTO obj)
        {
            List<string> errors = new();
            var model = MappingInsertDTO(obj);
            model.Active = true;
            model.CreatedTime = DateTime.Now;
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
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
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: insert list Permission
        /// </summary>
        /// <param name="objs">list Permission DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertPermissionDTO> objs)
        {
            List<string> errors = new List<string>();
            var models = objs.Select(c => MappingInsertDTO(c));
            foreach (var item in models)
            {
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
            }
            await _permissionRepository.CreateListAsync(models);
            await _permissionRepository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Descripton: Get list all Permission
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _permissionRepository.FindByConditionAsync(c=>c.Active);
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Get Permission by id
        /// </summary>
        /// <param name="id">Permission id</param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await _permissionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound($"Not found Permission with id: {id}", id);
            }
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: soft delete Permission by id
        /// </summary>
        /// <param name="id">Permission id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            await _permissionRepository.SoftDeleteAsync(id);
            await _permissionRepository.SaveChangesAsync();
            return DionResponse.Success(id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Soft delete list Permission
        /// </summary>
        /// <param name="objs">list Permission id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            foreach (var item in objs)
            {
                await _permissionRepository.SoftDeleteAsync(item);
            }
            await _permissionRepository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: update Permission
        /// </summary>
        /// <param name="obj">update Permission DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdatePermissionDTO obj)
        {
            List<string> errors = new();
            var model = await MappingUpdatePermissionDTO(obj);
            errors = await Validate(model);
            if (errors.Count > 0)
            {
                return DionResponse.BadRequest(errors);
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
            
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: update list Permission
        /// </summary>
        /// <param name="obj">list Permission DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePermissionDTO> obj)
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
                return DionResponse.BadRequest(errors);
            }
            await _permissionRepository.UpdateListAsync(updatePermissions);
            await _permissionRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Auhthor: TUNGTD
        /// Created: 27/07/2023
        /// Description: Maaping insert Permission DTO to Permission
        /// </summary>
        /// <param name="obj">Permission DTO object</param>
        /// <returns></returns>
        public Permission MappingInsertDTO(InsertPermissionDTO obj)
        {
            Permission Permission = _mapper.Map<Permission>(obj);
            Permission.Id = 0;
            Permission.Active = true;
            Permission.CreatedTime = DateTime.Now;
            return Permission;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// </summary>
        /// <param name="dto">update Permission DTO object</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found Permission id</exception>
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
        /// <summary>
        /// Author: TUNGTD
        /// Creatd: 27/07/2023
        /// Description: Validate to Permission
        /// </summary>
        /// <param name="obj">Permission object</param>
        /// <returns></returns>
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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get permission detail by id
        /// </summary>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        public async Task<DionResponse> Detail(int id)
        {
            var data = await _permissionRepository.Detail(id);
            return DionResponse.Success(data);
        }
    }
}
