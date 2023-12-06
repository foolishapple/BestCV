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
	public interface IExperienceRangeRepository : IRepositoryBaseAsync<ExperienceRange, int, JobiContext>
	{
		Task<bool> IsExperienceRangeExistAsync(string name, int id);

	}
}
