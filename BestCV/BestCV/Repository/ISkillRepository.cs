using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Skill;
using Jobi.Domain.Aggregates.TopFeatureJob;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ISkillRepository : IRepositoryBaseAsync<Skill, int, JobiContext>
    {
        Task<bool> IsNameExisAsync(string name, int id);
        Task<List<SkillAggregates>> searchSkills(Select2Aggregates select2Aggregates);
    }
}
