using BestCV.Application.Models.NotificationType;
using BestCV.Application.Models.PostType;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IPostTypeService : IServiceQueryBase<int, InsertPostTypeDTO, UpdatePostTypeDTO, PostTypeDTO>
    {
    }
}
