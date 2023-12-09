using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.TopCompany;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ITopCompanyRepository : IRepositoryBaseAsync<TopCompany, int, JobiContext>
    {
        Task<List<TopCompanyAggregates>> ListTopCompanyShowOnHomePageAsync();
        Task<List<SelectListItem>> ListCompanySelected();
        Task<TopCompany> GetByCompanyIdAsync(int companyId);
        Task<List<TopCompany>> FindByConditionAsync(Expression<Func<TopCompany, bool>> expression);
        Task<TopCompany> FindByCompanyIdAsync(int companyId);
        Task<TopCompany> FindByOrderSortAsync(int orderSort);
        Task<List<TopCompanyAggregates>> ListTopCompany();
        Task<int> MaxOrderSort(int orderSort);
        Task<bool> IsCompanyIdExist(long id, long companyId);
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task ChangeOrderSort(List<TopCompany> objs);
    }
}
