using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class ServicePackageGroupRepository : RepositoryBaseAsync<ServicePackageGroup, int, JobiContext>, IServicePackageGroupRepository
    {
        private readonly JobiContext dbContext;
    private readonly IUnitOfWork<JobiContext> unitOfWord;

    public ServicePackageGroupRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
    {
        this.dbContext = dbContext;
        this.unitOfWord = _unitOfWork;
    }
    /// <summary>
    /// Author : ThanhNd
    /// CreatedTime : 08/09/2023
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<bool> IsExistedAsync(int id, string name)
    {
        return await dbContext.ServicePackageGroups.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
    }
}
}
