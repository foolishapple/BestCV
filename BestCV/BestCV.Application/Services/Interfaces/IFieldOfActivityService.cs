using BestCV.Application.Models.FieldOfActivities;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface IFieldOfActivityService : IServiceQueryBase<int, InsertFieldOfActivityDTO, UpdateFieldOfActivityDTO, FieldOfActivityDTO>
	{

		public Task<BestCVResponse> LoadDataFilterFielOfActivityHomePageAsync();
	}
}
