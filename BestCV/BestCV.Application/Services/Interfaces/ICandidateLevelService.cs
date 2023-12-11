using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.Coupon;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateLevelService : IServiceQueryBase<int, InsertCandidateLevelDTO, UpdateCandidateLevelDTO, CandidateLevelDTO>
    {
    }
}
