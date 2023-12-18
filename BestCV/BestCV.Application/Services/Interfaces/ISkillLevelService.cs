using BestCV.Application.Models.SkillLevel;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ISkillLevelService : IServiceQueryBase<int, InsertSkillLevelDTO, UpdateSkillLevelDTO, SkillLevelDTO>
    {
        Task<BestCVResponse> UpdateAsync(UpdateSkillLevelDTO obj);
    }
}
