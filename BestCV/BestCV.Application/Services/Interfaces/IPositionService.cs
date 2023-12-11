using BestCV.Application.Models.Position;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface IPositionService : IServiceQueryBase<int, InsertPositionDTO, UpdatePositionDTO, PositionDTO>
    {

    }
}
