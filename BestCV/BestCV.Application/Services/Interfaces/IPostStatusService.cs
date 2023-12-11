using BestCV.Application.Models.PostLayout;
using BestCV.Application.Models.PostStatus;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IPostStatusService : IServiceQueryBase<int, InsertPostStatusDTO, UpdatePostStatusDTO, PostStatusDTO>
    {
    }
}
