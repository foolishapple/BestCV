using AutoMapper;
using BestCV.Application.Models.Menu;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly ILogger<IMenuService> logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider _serviceProvider;
        public MenuService(IMenuRepository _menuRepository, ILoggerFactory loggerFactory, IMapper _mapper, IRoleMenuRepository roleMenuRepository, IServiceProvider serviceProvider)
        {
            menuRepository = _menuRepository;
            logger = loggerFactory.CreateLogger<IMenuService>();
            mapper = _mapper;
            _roleMenuRepository = roleMenuRepository;
            _serviceProvider = serviceProvider;
        }


        public async Task<BestCVResponse> CreateAsync(InsertMenuDTO obj)
        {
            var menu = mapper.Map<Menu>(obj);
            menu.Active = true;
            menu.CreatedTime = DateTime.Now;
            menu.Description = !string.IsNullOrEmpty(menu.Description) ? menu.Description.ToEscape() : null;
            menu.TreeIds = " ";
            
            var listErrors = new List<string>();
            var isNameExist = await menuRepository.IsMenuExistAsync(menu.Name, 0, obj.MenuTypeId);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            using (var trans = await menuRepository.BeginTransactionAsync())
            {
                try
                {
                    await menuRepository.CreateAsync(menu);
                    await menuRepository.SaveChangesAsync();

                    if (obj.ParentId != null)
                    {
                        var parent = await menuRepository.FindByConditionAsync(x => x.Id == obj.ParentId);
                        menu.TreeIds = parent[0].TreeIds + "_" + menu.Id;
                    }
                    else
                    {
                        menu.TreeIds = menu.Id.ToString();
                    }
                    await menuRepository.UpdateTreeIdsMenu(menu);
                    await menuRepository.SaveChangesAsync();
                    if (obj.Roles.Count > 0)
                    {
                        var roleMenus = obj.Roles.Select(c => new RoleMenu()
                        {
                            Active = true,
                            CreatedTime = DateTime.Now,
                            MenuId = menu.Id,
                            RoleId = c
                        });
                        await _roleMenuRepository.CreateListAsync(roleMenus);
                        await _roleMenuRepository.SaveChangesAsync();      
                        
                    }
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    throw new Exception("Failed to create new menu");
                }
            }
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertMenuDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await menuRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MenuDTO>>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> GetAllMenuAdmin()
        {
            var data = await menuRepository.FindByConditionAsync(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_MENU_ADMIN);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MenuDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetAllMenuDashboardCandidate()
        {
            var data = await menuRepository.FindByConditionAsync(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_CANDIDATE);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MenuDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetAllMenuDashboardEmployer()
        {
            var data = await menuRepository.FindByConditionAsync(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_EMPLOYER);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MenuDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetAllMenuHeaderCandidate()
        {
            var data = await menuRepository.FindByConditionAsync(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_HEADER_CANDIDATE);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<MenuDTO>>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await menuRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<MenuDTO>(data);
            var roles = await _roleMenuRepository.FindByConditionAsync(c => c.MenuId == data.Id);
            HashSet<int> roleIds = roles.Select(c => c.RoleId).ToHashSet();
            result.Roles = roleIds;
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetListMenuHomepage()
        {
            var data = await menuRepository.ListMenuHomepage();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

  
        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await menuRepository.SoftDeleteAsync(id);
            if (data)
             {
                var menus = await menuRepository.FindByConditionAsync(x => x.TreeIds.StartsWith(id.ToString()));
                if(menus.Count > 0)
                {
                    var listIds = new List<int>();
                    foreach (var menu in menus)
                    {
                        listIds.Add(menu.Id);
                    }
                    bool isDelete = await menuRepository.SoftDeleteListAsync(listIds);
                    if (!isDelete)
                    {
                        return BestCVResponse.BadRequest("Không thể xóa menu, hãy thử lại sau");
                    }
                }
                await menuRepository.SaveChangesAsync();

                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateMenuDTO obj)
        {
            var menu = await menuRepository.GetByIdAsync(obj.Id);
            if (menu == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateMenu = mapper.Map(obj, menu);
            menu.Description = !string.IsNullOrEmpty(menu.Description) ? menu.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await menuRepository.IsMenuExistAsync(updateMenu.Name, updateMenu.Id, updateMenu.MenuTypeId);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            using(var trans = await menuRepository.BeginTransactionAsync())
            {
                try
                {
                    await menuRepository.UpdateAsync(updateMenu);
                    await menuRepository.SaveChangesAsync();

                    var roles = await _roleMenuRepository.FindByConditionAsync(c => c.Active && c.MenuId == menu.Id);
                    IEnumerable<int> listDelete = roles.Where(c => !obj.Roles.Contains(c.RoleId)).Select(c => c.Id);
                    List<RoleMenu> listAdd = obj.Roles.Select(c => new RoleMenu()
                    {
                        Active = true,
                        MenuId = menu.Id,
                        CreatedTime = DateTime.Now,
                        RoleId = c
                    }).Where(c => !roles.Select(g => g.RoleId).Contains(c.RoleId)).ToList();
                    if (listDelete.Count() > 0)
                    {
                        await _roleMenuRepository.HardDeleteListAsync(listDelete);
                    }
                    if (listAdd.Count() > 0)
                    {
                        await _roleMenuRepository.CreateListAsync(listAdd);
                    }
                    await _roleMenuRepository.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    throw new Exception("Failed to update menu");
                }
            }
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateMenuDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
