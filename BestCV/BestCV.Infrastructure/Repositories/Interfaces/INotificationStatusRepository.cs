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
    public interface INotificationStatusRepository : IRepositoryBaseAsync<NotificationStatus, int, JobiContext>
    {
        /// <summary>
        /// Description: Check notification status name is existed
        /// </summary>
        /// <param name="name">notification status name</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Description: Check notification status color is existed
        /// </summary>
        /// <param name="color">notification status color</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        Task<bool> ColorIsExisted(string color, int id);
    }
}
