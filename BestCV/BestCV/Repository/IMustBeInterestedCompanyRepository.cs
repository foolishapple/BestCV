using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.MustBeInterestedCompany;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IMustBeInterestedCompanyRepository : IRepositoryBaseAsync<MustBeInterestedCompany, long, JobiContext>
    {
        Task<List<MustBeInterestedCompanyAggregates>> ListAggregatesAsync();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<SelectListItem>> ListCompanySelected();
        Task<List<MustBeInterestedCompanyAggregates>> ListCompanyInterestedOnDetailJob();
    }
}
