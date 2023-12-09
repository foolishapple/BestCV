using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Skill;
using BestCV.Domain.Aggregates.TopFeatureJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ISkillRepository : IRepositoryBaseAsync<Skill, int, JobiContext>
    {
        Task<bool> IsNameExisAsync(string name, int id);
        Task<List<SkillAggregates>> searchSkills(Select2Aggregates select2Aggregates);
    }
}
