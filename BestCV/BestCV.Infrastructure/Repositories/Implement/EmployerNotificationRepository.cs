using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerNotification;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerNotificationRepository : RepositoryBaseAsync<EmployerNotification, long, JobiContext>, IEmployerNotificationRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public EmployerNotificationRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        public async Task<DTResult<EmployerNotification>> ListEmployerNotificationByEmployerIdAsync(EmployerNotificationParameter parameters, long employerId)
        {
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            var query = (
                from c in db.EmployerNotifications
                join nt in db.NotificationTypes on c.NotificationTypeId equals nt.Id
                join ns in db.NotificationStatuses on c.NotificationStatusId equals ns.Id
                where (c.EmployerId == employerId && c.Active && nt.Active && ns.Active)
                select new EmployerNotification
                {
                    NotificationStatusId = c.NotificationStatusId,
                    Active = c.Active,
                    CreatedTime = c.CreatedTime,
                    EmployerId = c.EmployerId,
                    Name = c.Name,
                    Link = c.Link,
                    Description = c.Description,
                    Id = c.Id,
                    NotificationTypeId = c.NotificationTypeId
                });

            if (parameters.EmployerId != null)
            {
                query = query.Where(c => c.EmployerId == parameters.EmployerId);
            }
            var recordsTotal = await query.CountAsync();

            if (!String.IsNullOrEmpty(keyword))
            {
                string noneVietnameseKeyword = keyword.ToLower().RemoveVietnamese();
                query = query.Where(e => 
                    EF.Functions.Collate(e.Name, SQLParams.Latin_General).Contains(EF.Functions.Collate(keyword, SQLParams.Latin_General))
                    || e.CreatedTime.ToCustomString().Contains(noneVietnameseKeyword));
            }
            int recordsFiltered = await query.CountAsync();
            var data = await query.OrderByDescending(c => c.CreatedTime).Skip(parameters.Start).Take(parameters.Length).ToListAsync();
            DTResult<EmployerNotification> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
        public async Task MakeAsRead(EmployerNotification obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.NotificationStatusId).IsModified = true;
            await db.SaveChangesAsync();
        }
        public async Task<int> CountByEmployerId(long employerId, long? statusId)
        {
            var query = db.EmployerNotifications.Where(c => c.Employer.Active && c.Active && c.NotificationStatus.Active && c.NotificationType.Active && c.EmployerId == employerId);
            if (statusId != null)
            {
                query = query.Where(c => c.NotificationStatusId == statusId);

            }
            var total = await query.CountAsync();
            return total;
        }
        
        public async Task<List<EmployerNotification>> ListRecented(long id)
        {
            return await db.EmployerNotifications.Where(c => c.Active && c.NotificationType.Active && c.NotificationStatus.Active && c.EmployerId == id).Select(c => new EmployerNotification()
            {
                NotificationStatusId = c.NotificationStatusId,
                Active = c.Active,
                CreatedTime = c.CreatedTime,
                Description = c.Description,
                EmployerId = c.EmployerId,
                Name = c.Name,
                Id = c.Id,
                NotificationTypeId = c.NotificationTypeId,
                Link = c.Link,
            }).OrderByDescending(c => c.CreatedTime).Take(NotificationConfig.MaxShowNumber).ToListAsync();
        }
    }
}
