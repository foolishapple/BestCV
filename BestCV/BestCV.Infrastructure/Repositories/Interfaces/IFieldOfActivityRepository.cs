using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
	public interface IFieldOfActivityRepository : IRepositoryBaseAsync<FieldOfActivity, int, JobiContext>
	{
		/// <summary>
		/// Description: Check Field Of Activityname is existed
		/// </summary>
		/// <param name="name">Field Of Activityname</param>
		/// <param name="id">Field Of Activityid</param>
		/// <returns></returns>
		Task<bool> NameIsExisted(string name, int id);

		/// <summary>
		/// Description: lấy dữ liệu và đếm xem có bảo nhiêu nhà tuyển dụng có lĩnh vực này
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public Task<object> LoadDataFilterFielOfActivityHomePageAsync();
	}
}
