using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.AdminAccounts;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IAdminAccountRepository:IRepositoryBaseAsync<AdminAccount,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 25/07/2003
        /// Description: Check username admin account is existed
        /// </summary>
        /// <param name="username"></param>
        /// <param name="adminAccountId"></param>
        /// <returns></returns>
        Task<bool> UserNameIsExist(string username,long adminAccountId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2003
        /// Description: Check email admin account is existed
        /// </summary>
        /// <param name="email"></param>
        /// <param name="adminAccountid"></param>
        /// <returns></returns>
        Task<bool> EmailIsExisted(string email, long adminAccountId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2003
        /// Description: Check phone number admin account is existed
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="adminAccountId"></param>
        /// <returns></returns>
        Task<bool> PhoneIsExisted(string phone, long adminAccountId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Get admin account by user name
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns></returns>
        Task<AdminAccount?> FindByUserName(string userName);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<AdminAccount?> FindByEmail(string email);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Check password is valid
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="id">admin account id</param>
        /// <returns></returns>
        Task<bool> IsValidPassword(string password, long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: update password to admin account
        /// </summary>
        /// <param name="adminAccount">admin account object</param>
        /// <returns></returns>
        Task UpdatePassWord(AdminAccount adminAccount);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: List all admin account aggregate
        /// </summary>
        /// <returns></returns>
        Task<List<AdminAccountAggregate>> ListAllAggregate();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get addmin account aggregate detail by id
        /// </summary>
        /// <param name="id">admin accout id</param>
        /// <returns></returns>
        Task<AdminAccountAggregate> DetailAggregate(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 09/08/2023
        /// Description: Get admin account by usename and password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<AdminAccount?> DetailSignIn(string user,string pass);
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        Task<bool> CheckEmailIsActive(string email);
    }
}
