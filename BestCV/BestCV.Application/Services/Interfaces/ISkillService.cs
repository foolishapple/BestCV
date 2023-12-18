using BestCV.Application.Models.Skill;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.TopFeatureJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ISkillService : IServiceQueryBase<int, InsertSkillDTO, SkillDTO, UpdateSkillDTO>
    {
        Task<BestCVResponse> UpdateAsync(UpdateSkillDTO obj);
        Task<BestCVResponse> searchSkills(Select2Aggregates select2Aggregates);
    }
}
