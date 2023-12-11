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
		/// <summary>
		/// Author: HuyDQ
		/// Created: 16/08/2023
		/// Description: lấy dữ liệu và đếm xem có bảo nhiêu nhà tuyển dụng có lĩnh vực này
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public Task<DionResponse> LoadDataFilterFielOfActivityHomePageAsync();
	}
}
