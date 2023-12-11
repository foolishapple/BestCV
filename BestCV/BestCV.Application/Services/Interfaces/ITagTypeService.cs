using BestCV.Application.Models.TagType;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITagTypeService : IServiceQueryBase<int, InsertTagTypeDTO, UpdateTagTypeDTO, TagTypeDTO>
    {
    }
}
