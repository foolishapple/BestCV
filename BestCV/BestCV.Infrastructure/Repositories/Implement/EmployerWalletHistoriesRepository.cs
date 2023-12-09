using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.EmployerWalletHistories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerWalletHistoriesRepository : RepositoryBaseAsync<EmployerWalletHistory, long, JobiContext>, IEmployerWalletHistoriesRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerWalletHistoriesRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author : Thoại Anh 
        /// CreatedTime : 04/10/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ListEmployerWalletHistories(DTParameters parameters)
        {
            var keyword = parameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            if (parameters.Order != null)
            {
                orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }
            var result = await (
               from ewh in db.EmployerWalletHistories
               join ew in db.EmployerWallets on ewh.EmployerWalletId equals ew.Id
               join e in db.Employers on ew.EmployerId equals e.Id
               join wht in db.WalletHistoryTypes on ewh.WalletHistoryTypeId equals wht.Id
               join c in db.Candidates on ewh.CandidateId equals c.Id
               join cp in db.Companies on e.Id equals cp.EmployerId
               where ewh.Active && ew.Active && e.Active && wht.Active && c.Active && cp.Active
               select new EmployerWalletHistoriesAggregate
               {
                   Id = ewh.Id,
                   Description = ewh.Description,
                   CompanyName = cp.Name,
                   EmployerId = e.Id,
                   EmployerName = e.Fullname,
                   EmployerEmail = e.Email,
                   EmployerPhone = e.Phone,
                   CandidateId = c.Id,
                   CandidateEmail = c.Email,
                   CandidateName = c.FullName,
                   CandidatePhone = c.Phone,
                   UpdatedTime = ewh.UpdatedTime,
                   IsApproved = ewh.IsApproved



               }).ToListAsync();
            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword)
       || (s.EmployerName != null && s.EmployerName.ToString().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.EmployerEmail != null && s.EmployerEmail.ToString().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.EmployerPhone != null && s.EmployerPhone.ToString().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.CandidateEmail != null && s.CandidateEmail.ToString().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.CandidateName != null && s.CandidateName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.CandidatePhone != null && s.CandidatePhone.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (s.CompanyName != null && (s.CompanyName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || (!string.IsNullOrEmpty(s.Description) && s.Description.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))
       || s.UpdatedTime.ToCustomString().Contains(keyword))).ToList();

            foreach (var column in parameters.Columns)
            {
                var search = column.Search.Value;
                if (!search.Contains("/"))
                {
                    search = column.Search.Value.ToLower().RemoveVietnamese();
                }
                if (string.IsNullOrEmpty(search)) continue;
                switch (column.Data)
                {
                    case "companyName":
                        result = result.Where(r => r.EmployerName.ToLower().RemoveVietnamese().Contains(search) || r.EmployerPhone.ToLower().RemoveVietnamese().Contains(search) || r.EmployerEmail.ToLower().RemoveVietnamese().Contains(search) || r.CompanyName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                              
                        break;
                    case "candidateName":
                        result = result.Where(r => r.CandidateName.ToLower().RemoveVietnamese().Contains(search) || r.CandidateEmail.ToLower().RemoveVietnamese().Contains(search) || r.CandidatePhone.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "description":
                        result = result.Where(r => !string.IsNullOrEmpty(r.Description) && r.Description.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "updatedTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.UpdatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.UpdatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.UpdatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.UpdatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }

                        break;
                    case "isApproved":
                        if (search != "null")
                        {
                            result = result.Where(r => r.IsApproved.ToString().ToLower().Contains(search)).ToList();
                        }
                        break;
                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();
            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                allData = result,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }
        public async Task<EmployerWalletHistoriesAggregate> GetByidAggregateAsync(long id)
        {
            var result = await (
              from ewh in db.EmployerWalletHistories
              join ew in db.EmployerWallets on ewh.EmployerWalletId equals ew.Id
              join e in db.Employers on ew.EmployerId equals e.Id
              join wht in db.WalletHistoryTypes on ewh.WalletHistoryTypeId equals wht.Id
              join c in db.Candidates on ewh.CandidateId equals c.Id
              join cp in db.Companies on e.Id equals cp.EmployerId
              where ewh.Active && ew.Active && e.Active && wht.Active && c.Active && cp.Active && ewh.Id == id
              select new EmployerWalletHistoriesAggregate
              {
                  Id = ewh.Id,
                  Amount = ewh.Amount,
                  EmployerWalletId = ewh.EmployerWalletId,
                  WalletHistoryTypeId = ewh.WalletHistoryTypeId,
                  Description = ewh.Description,
                  CompanyName = cp.Name,
                  EmployerId = e.Id,
                  EmployerName = e.Fullname,
                  EmployerEmail = e.Email,
                  EmployerPhone = e.Phone,
                  CandidateId = c.Id,
                  CandidateEmail = c.Email,
                  CandidateName = c.FullName,
                  CandidatePhone = c.Phone,
                  UpdatedTime = ewh.UpdatedTime,
                  IsApproved = ewh.IsApproved,
                  Name = ewh.Name


              }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> QuickIsApprovedAsync(long id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.IsApproved = obj.IsApproved ? false : true;

                db.Attach(obj);
                db.Entry(obj).Property(x => x.IsApproved).IsModified = true;
                return true;
            }
            return false;
        }
    }
}
