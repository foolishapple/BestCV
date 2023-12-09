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
    public interface INotificationTypeRepository : IRepositoryBaseAsync<NotificationType, int, JobiContext>
    {
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">NotificationTypeId</param>
        /// <param name="name">NotificationTypeName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

    }
}
