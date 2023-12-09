using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.MustBeInterestedCompany;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IMustBeInterestedCompanyRepository : IRepositoryBaseAsync<MustBeInterestedCompany, long, JobiContext>
    {
        Task<List<MustBeInterestedCompanyAggregates>> ListAggregatesAsync();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<SelectListItem>> ListCompanySelected();
        Task<List<MustBeInterestedCompanyAggregates>> ListCompanyInterestedOnDetailJob();
    }
}
