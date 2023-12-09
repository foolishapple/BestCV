using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.License;
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
    public class LicenseRepository : RepositoryBaseAsync<License, long, JobiContext>, ILicenseRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWord;
        public LicenseRepository(JobiContext _db, IUnitOfWork<JobiContext> _unitOfWork) : base(_db, _unitOfWork)
        {
            this.db = _db;
            this.unitOfWord = _unitOfWork;
        }

        public async Task<List<LicenseAggregates>> GetListByCompanyId(int companyId)
        {
            var data = await (
                from l in db.Licenses
                join lt in db.LicenseTypes on l.LicenseTypeId equals lt.Id
                where (l.CompanyId == companyId && l.Active && lt.Active)
                select new LicenseAggregates
                {
                    Id = l.Id,
                    CompanyId = l.CompanyId,
                    LicenseTypeId = l.LicenseTypeId,
                    LicenseTypeName = lt.Name,
                    Path = l.Path,
                    IsApproved = l.IsApproved,
                    ApprovalDate = l.ApprovalDate,
                    Active = l.Active,
                    CreatedTime = l.CreatedTime
                }).ToListAsync();
            return data;
        }

        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 07/09/2023
        /// Description : Lấy danh sách giầy tờ nhà tuyển dụng (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        public async Task<object> ListLicenseAggregates(DTParameters parameters)
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

            var result = await(
                from l in db.Licenses
                join lt in db.LicenseTypes on l.LicenseTypeId equals lt.Id
                join c in db.Companies on l.CompanyId equals c.Id

                where l.Active && lt.Active && c.Active
                select new LicenseAggregates
                {
                    Id = l.Id,
                    Active = l.Active,
                    CreatedTime = l.CreatedTime,
                    LicenseTypeId = l.LicenseTypeId,
                    LicenseTypeName = lt.Name,
                    CompanyId = l.CompanyId,
                    CompanyName = c.Name,
                    Path = l.Path,
                    IsApproved = l.IsApproved,
                    ApprovalDate = l.ApprovalDate,
                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword) || 
                                  s.Path.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || 
                                  s.CompanyName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese())).ToList();


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
                    case "path":
                        result = result.Where(r => r.Path.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "companyName":
                        result = result.Where(r => r.CompanyName.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "licenseTypeName":
                        var licenseTypeNameArr = search.Split(",").Select(n => Int32.Parse(n));
                        result = result.Where(r => licenseTypeNameArr.Contains(r.LicenseTypeId)).ToList();
                        break;
                    case "createdTime":
                        var searchDateArrs = search.Split(',');

                        if (searchDateArrs.Length == 2)
                        {
                            //Không có ngày bắt đầu
                            if (string.IsNullOrEmpty(searchDateArrs[0]))
                            {
                                result = result.Where(r => r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                            //không có ngày kết thúc
                            else if (string.IsNullOrEmpty(searchDateArrs[1]))
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0])).ToList();
                            }
                            //có cả 2
                            else
                            {
                                result = result.Where(r => r.CreatedTime >= Convert.ToDateTime(searchDateArrs[0]) && r.CreatedTime <= Convert.ToDateTime(searchDateArrs[1])).ToList();
                            }
                        }
                        break;
                    case "isApproved":
                        if (search != "null")
                        {
                            result = result.Where(r => search.Contains(r.IsApproved ? "true" : "false")).ToList();
                        }
                        break;             
                }
            }
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();
            var data = new
            {
                draw = parameters.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = result.Count,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
            return data;
        }

        /// <summary>
        /// Author: HuyDQ
        /// CreatedAt: 07/09/2023
        /// Description: approve license
        /// </summary>
        /// <param name="obj">License</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateApproveStatusLicenseAsync(License obj)
        {
            db.Attach(obj);
            db.Entry(obj).Property(c => c.IsApproved).IsModified = true;
            db.Entry(obj).Property(c => c.ApprovalDate).IsModified = true;
            return await db.SaveChangesAsync() > 0;
        }
    }
}
