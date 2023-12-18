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

        public Task<BestCVResponse> CreateAsync(InsertRolePermissionDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertRolePermissionDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var items = await _repository.FindByConditionAsync(c => c.Active);
            return BestCVResponse.Success(items);
        }

        public Task<BestCVResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateRolePermissionDTO obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateList(UpdateListRolePermissionDTO obj)
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
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateRolePermissionDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
