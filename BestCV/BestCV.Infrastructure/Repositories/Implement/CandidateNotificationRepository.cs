using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateNotification;
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
using System.Xml.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateNotificationRepository : RepositoryBaseAsync<CandidateNotification, long ,JobiContext >,ICandidateNotificationRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateNotificationRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }

        /// <summary>
        /// Author : Thoại Anh 
        /// CreatedDate: 27/07/2023
        /// Description : Lấy ra thông báo theo từng ứng viên 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<DTResult<CandidateNotification>> ListCandidateNotificationByIdAsync(CandidateNotificationParameter parameters, long candidateId)
        {
            string keyword = "";
            if (parameters.Search != null)
            {
                keyword = parameters.Search.Value.Trim();
            }
            var query = (
                from c in dbContext.CandidateNotifications
                join nt in dbContext.NotificationTypes on c.NotificationTypeId equals nt.Id
                join ns in dbContext.NotificationStatuses on c.NotificationStatusId equals ns.Id
                where (c.CandidateId == candidateId && c.Active && nt.Active && ns.Active)
                select new CandidateNotification
                {
                    NotificationStatusId = c.NotificationStatusId,
                    Active = c.Active,
                    CreatedTime = c.CreatedTime,
                    CandidateId = c.CandidateId,
                    Name = c.Name,
                    Link = c.Link,
                    Description = c.Description,
                    Id = c.Id,
                    NotificationTypeId = c.NotificationTypeId
                });

            if (parameters.CandidateId != null)
            {
                query = query.Where(c => c.CandidateId == parameters.CandidateId);
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
            DTResult<CandidateNotification> result = new()
            {
                draw = parameters.Draw,
                data = data,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal
            };
            return result;
        }
        public async Task MakeAsRead(CandidateNotification obj)
        {
            dbContext.Attach(obj);
            dbContext.Entry(obj).Property(c => c.NotificationStatusId).IsModified = true;
            await dbContext.SaveChangesAsync();
        }
        public async Task<int> CountByCandidateId(long candidateId, long? statusId)
        {
            var query = dbContext.CandidateNotifications.Where(c => c.Candidate.Active && c.Active && c.NotificationStatus.Active && c.NotificationType.Active && c.CandidateId == candidateId);
            if(statusId != null)
            {
                query = query.Where(c => c.NotificationStatusId == statusId);

            }
            var total = await query.CountAsync();  
            return total;
        }
        public async Task<List<CandidateNotification>> ListRecented(long id)
        {
            return await dbContext.CandidateNotifications.Where(c => c.Active && c.NotificationType.Active && c.NotificationStatus.Active && c.CandidateId == id).Select(c => new CandidateNotification()
            {
                NotificationStatusId = c.NotificationStatusId,
                Active = c.Active,
                CreatedTime = c.CreatedTime,
                Description = c.Description,
                CandidateId = c.CandidateId,
                Name = c.Name,
                Id = c.Id,
                NotificationTypeId = c.NotificationTypeId,
                Link = c.Link,
            }).OrderByDescending(c => c.CreatedTime).Take(NotificationConfig.MaxShowNumber).ToListAsync();
        }
    }
}
