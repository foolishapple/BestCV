using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class NotificationStatusRepository : RepositoryBaseAsync<NotificationStatus, int, JobiContext>, INotificationStatusRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public NotificationStatusRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check notification status name is existed
        /// </summary>
        /// <param name="name">notification status name</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.NotificationStatuses.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check notification status color is existed
        /// </summary>
        /// <param name="color">notification status color</param>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        public async Task<bool> ColorIsExisted(string color, int id)
        {
            return await _db.NotificationStatuses.AnyAsync(c => c.Color == color && c.Active && c.Id != id);
        }
    }
}
