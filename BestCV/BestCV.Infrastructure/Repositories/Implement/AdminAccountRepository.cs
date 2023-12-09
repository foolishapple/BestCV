using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.AdminAccounts;
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
    public class AdminAccountRepository : RepositoryBaseAsync<AdminAccount, long, JobiContext>, IAdminAccountRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public AdminAccountRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            _db = dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get addmin account aggregate detail by id
        /// </summary>
        /// <param name="id">admin accout id</param>
        /// <returns></returns>
        public async Task<AdminAccountAggregate> DetailAggregate(long id)
        {
            var query = from a in _db.AdminAccounts
                        where a.Active
                        select new AdminAccountAggregate()
                        {
                            CreatedTime = a.CreatedTime,
                            Email = a.Email,
                            Id = a.Id,
                            Phone = a.Phone,
                            UserName = a.UserName,
                            Description = a.Description,
                            FullName = a.FullName,
                            LockEnabled = a.LockEnabled,
                            Photo =a.Photo,
                            Roles = (from ar in _db.AdminAccountRoles
                                     join r in _db.Roles on ar.RoleId equals r.Id
                                     where ar.AdminAccountId == a.Id && r.Active && ar.Active
                                     select r.Id).ToHashSet()
                        };
            return await query.FirstAsync(c => c.Id == id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 09/08/2023
        /// Description: Get admin account by usename and password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public async Task<AdminAccount?> DetailSignIn(string user, string pass)
        {
            var data = await _db.AdminAccounts.FirstOrDefaultAsync(c => c.Active && c.UserName == user && c.Password == pass);
            return data;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check email is existed
        /// </summary>
        /// <param name="email"></param>
        /// <param name="adminAccountId"></param>
        /// <returns></returns>
        public async Task<bool> EmailIsExisted(string email, long adminAccountId)
        {
            return await _db.AdminAccounts.AnyAsync(c => c.Email == email && c.Active && c.Id != adminAccountId);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Find admin account by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<AdminAccount?> FindByEmail(string email)
        {
            return await _db.AdminAccounts.FirstOrDefaultAsync(c => c.Email == email && c.Active);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AdminAccount?> FindByUserName(string userName)
        {
            return await _db.AdminAccounts.FirstOrDefaultAsync(c => c.UserName == userName && c.Active);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Check password is valid
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="id">admin account id</param>
        /// <returns></returns>
        public async Task<bool> IsValidPassword(string password, long id)
        {
            return await _db.AdminAccounts.AnyAsync(c => c.Password == password && c.Active && c.Id == id);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: List all admin account aggregate
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdminAccountAggregate>> ListAllAggregate()
        {
            var query = from a in _db.AdminAccounts
                        where a.Active
                        select new AdminAccountAggregate()
                        {
                            CreatedTime = a.CreatedTime,
                            Email = a.Email,
                            Id = a.Id,
                            Phone = a.Phone,
                            UserName = a.UserName,
                            Roles = (from ar in _db.AdminAccountRoles
                                     join r in _db.Roles on ar.RoleId equals r.Id
                                     where ar.AdminAccountId == a.Id && r.Active && ar.Active
                                     select r.Id).ToHashSet()
                        };
            var data = await query.ToListAsync();
            return data;
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check phone number is existed
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="adminAccountId"></param>
        /// <returns></returns>
        public async Task<bool> PhoneIsExisted(string phone, long adminAccountId)
        {
            return await _db.AdminAccounts.AnyAsync(c => c.Phone == phone && c.Active && c.Id != adminAccountId);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Update password to admin account
        /// </summary>
        /// <param name="adminAccount"></param>
        /// <returns></returns>
        public async Task UpdatePassWord(AdminAccount adminAccount)
        {
            _db.Attach(adminAccount);
            _db.Entry(adminAccount).Property(c => c.Password).IsModified = true;
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check username is existed
        /// </summary>
        /// <param name="username"></param>
        /// <param name="adminAccountId"></param>
        /// <returns></returns>
        public async Task<bool> UserNameIsExist(string username, long adminAccountId)
        {
            return await _db.AdminAccounts.AnyAsync(c => c.UserName == username && c.Active && c.Id != adminAccountId);
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await _db.AdminAccounts.AnyAsync(e => e.Email.Equals(email) && e.Active);
        }
    }
}
