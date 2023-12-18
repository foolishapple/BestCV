using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Entities;
using BestCV.Core.Entities;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICVTemplateService : IServiceQueryBase<long, CVTemplate, CVTemplate, CVTemplate>
    {
        Task<BestCVResponse> GetAllPublishAsync();
    }
}
