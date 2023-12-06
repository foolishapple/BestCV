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
    public interface INotificationStatusRepository : IRepositoryBaseAsync<NotificationStatus, int, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Check notification status name is existed
        /// </summary>
        /// <param name="name">notification status name</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Check notification status color is existed
        /// </summary>
        /// <param name="color">notification status color</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        Task<bool> ColorIsExisted(string color, int id);
    }
}
