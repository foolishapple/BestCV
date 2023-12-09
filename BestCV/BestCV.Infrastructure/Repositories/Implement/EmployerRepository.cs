using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Candidate;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerRepository : RepositoryBaseAsync<Employer, long, JobiContext>, IEmployerRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<Employer> FindByEmail(string email)
        {
            var obj = (await FindByConditionAsync(x => x.Email == email)).FirstOrDefault();
            return obj;
        }

        public async Task<bool> EmailExisted(string email)
        {
            var result = await db.Employers.Where(e => e.Email == email).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<bool> PhonedExisted(string phone)
        {
            var result = await db.Employers.Where(e => e.Phone == phone).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<bool> SkypeAccountExisted(string skypeAccount)
        {
            var result = await db.Employers.Where(e => e.Email == skypeAccount).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<bool> UsernameExisted(string username)
        {
            var result = await db.Employers.Where(e => e.Email == username).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Lấy thông tin nhà tuyển dụng theo Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        public async Task<Employer?> GetByEmailAsync(string email)
        {
            return await db.Employers.Where(e => e.Email.Equals(email) && e.Active).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Lấy thông tin nhà tuyển dụng theo số điện thoại
        /// </summary>
        /// <param name="email">phone</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        public async Task<Employer?> GetByPhoneAsync(string phone)
        {
            return await db.Employers.Where(e => e.Phone.Equals(phone) && e.Active).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Author: ThanhNd
        /// CreatedTime : 03/08/2023
        /// Description : Lấy danh sách NTD (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        public async Task<object> ListEmployerAggregates(DTParameters parameters)
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
                from e in db.Employers
                join es in db.AccountStatuses on e.EmployerStatusId equals es.Id
                join p in db.Positions on e.PositionId equals p.Id

                where e.Active && es.Active  && p.Active
                select new EmployerAggregates
                {
                    Id = e.Id,
                    Active = e.Active,
                    CreatedTime = e.CreatedTime,
                    Description = e.Description,
                    PositionId = e.PositionId,
                    Email = e.Email,
                    EmployerServicePackageEfficiencyExpiry = e.EmployerServicePackageEfficiencyExpiry,
                    EmployerStatusId = e.EmployerStatusId,
                    EmployerStatusName = es.Name,
                    EmployerStatusColor = es.Color,
                    Fullname = e.Fullname,
                    Gender = e.Gender,
                    IsActivated = e.IsActivated,
                    Password = e.Password,
                    Phone = e.Phone,
                    Photo = e.Photo,
                    PositionName = p.Name,
                    Search = e.Search,
                    SkypeAccount = e.SkypeAccount,
                    Username = e.Username,
                }).ToListAsync();

            var recordsTotal = result.Count;
            result = result.Where(s => string.IsNullOrEmpty(keyword) || s.Fullname.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.Username.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.EmployerStatusName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.EmployerServicePackageName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || s.PositionName.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()) || (!string.IsNullOrEmpty(s.Search) && s.Search.ToLower().RemoveVietnamese().Contains(keyword.ToLower().RemoveVietnamese()))).ToList();


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
                    case "fullname":
                        result = result.Where(r => r.Fullname.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "username":
                        result = result.Where(r => r.Username.ToLower().RemoveVietnamese().Contains(search)).ToList();
                        break;
                    case "employerServicePackage":
                        var employerServicePackageArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => employerServicePackageArr.Contains(r.EmployerServicePackageId)).ToList();
                        break;
                    case "employerStatusName":
                        var employerStatusIdsArr = search.Split(",").Select(n => Int64.Parse(n));
                        result = result.Where(r => employerStatusIdsArr.Contains(r.EmployerStatusId)).ToList();
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
                    case "isActivated":
                        if (search != "null")
                        {
                            result = result.Where(r => r.IsActivated.ToString().ToLower().Contains(search)).ToList();
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

        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime: 04/08/2023
        /// Description: Change password for admin page
        /// </summary>
        /// <param name="obj">Employer</param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(Employer obj)
        {
            if (obj != null)
            {
                db.Attach(obj);
                db.Entry(obj).Property(x => x.Password).IsModified = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Author : ThanhND 
        /// CreatedTime : 04/08/2023
        /// </summary>
        /// <param name="id">employerId</param>
        /// <returns></returns>
        public async Task<bool> QuickActivatedAsync(long id)
        {
            var obj = await GetByIdAsync(id);
            if (obj != null)
            {
                obj.IsActivated = obj.IsActivated ? false : true;

                db.Attach(obj);
                db.Entry(obj).Property(x => x.IsActivated).IsModified = true;
                return true;
            }
            return false;
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        public async Task<bool> CheckEmailIsActive (string email)
        {
            return await db.Employers.AnyAsync(e => e.Email.Equals(email) && e.Active);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Desciption: Check job is of employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<bool> PrivacyJob(long employerId, long jobId)
        {
            var query = from j in db.Jobs
                        join rc in db.RecruitmentCampaigns on j.RecruimentCampaignId equals rc.Id
                        join e in db.Employers on rc.EmployerId equals e.Id
                        where j.Active && rc.Active && e.Active && j.Id == jobId && e.Id == employerId
                        select j;
            var result = await query.AnyAsync();
            return result;
        }
    }
}
