using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.CVTemplate;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICVTemplateRepository : IRepositoryBaseAsync<CVTemplate, long, JobiContext>
    {
        Task<List<CVTemplateListAggregate>> GetAllPublishAsync();
    }
}
