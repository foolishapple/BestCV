using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
	public interface IFieldOfActivityRepository : IRepositoryBaseAsync<FieldOfActivity, int, JobiContext>
	{
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Check Field Of Activityname is existed
		/// </summary>
		/// <param name="name">Field Of Activityname</param>
		/// <param name="id">Field Of Activityid</param>
		/// <returns></returns>
		Task<bool> NameIsExisted(string name, int id);

		/// <summary>
		/// Author: HuyDQ
		/// Created: 16/08/2023
		/// Description: lấy dữ liệu và đếm xem có bảo nhiêu nhà tuyển dụng có lĩnh vực này
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public Task<object> LoadDataFilterFielOfActivityHomePageAsync();
	}
}
