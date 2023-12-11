using AutoMapper;
using BestCV.Application.Models.RolePermissions;
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
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _repository;
        private readonly IMapper _mapper; 
        private readonly ILogger _logger;
        public RolePermissionService(IRolePermissionRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<RolePermissionService>();
            _mapper = mapper;
        }

        public Task<DionResponse> CreateAsync(InsertRolePermissionDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertRolePermissionDTO> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: get list all role permission
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var items = await _repository.FindByConditionAsync(c => c.Active);
            return DionResponse.Success(items);
        }

        public Task<DionResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateRolePermissionDTO obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 09/08/2023
        /// Description: Update list role permission DTO
        /// </summary>
        /// <param name="obj">update list role permission DTO object</param>
        /// <returns></returns>
        /// <exception cref="Exception">Failed update</exception>
        public async Task<DionResponse> UpdateList(UpdateListRolePermissionDTO obj)
        {
            using(var trans = await _repository.BeginTransactionAsync())
            {
                try
                {
                    if (obj.AddItems != null && obj.AddItems.Count > 0)
                    {
                        var addItems = obj.AddItems.Select(c => _mapper.Map<RolePermission>(c));
                        foreach(var item in addItems)
                        {
                            item.Active = true;
                            item.CreatedTime = DateTime.Now;
                        }
                        await _repository.CreateListAsync(addItems);
                    }
                    if (obj.DeleteItems != null && obj.DeleteItems.Count > 0)
                    {
                        await _repository.HardDeleteListAsync(obj.DeleteItems);
                    }
                    await _repository.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    throw new Exception("Failed to update list role permission");
                }
            }
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateRolePermissionDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
