using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerRepository : IRepositoryBaseAsync<Employer, long, JobiContext>
    {
        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="username"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra username đã tồn tại hay chưa</returns>
        Task<bool> UsernameExisted(string username);

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra email đã tồn tại hay chưa</returns>
        Task<bool> EmailExisted(string email);

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra số điện thoại đã tồn tại hay chưa</returns>
        Task<bool> PhonedExisted(string phone);

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="skypeAccount"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra tài khoản skype đã tồn tại hay chưa</returns>
        Task<bool> SkypeAccountExisted(string skypeAccount);

        Task<Employer> FindByEmail(string email);
        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Lấy thông tin nhà tuyển dụng theo Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        Task<Employer?> GetByEmailAsync(string email);

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 28/07/2023
        /// Description: Lấy thông tin nhà tuyển dụng theo số điện thoại
        /// </summary>
        /// <param name="email">phone</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        Task<Employer?> GetByPhoneAsync(string phone);
        /// <summary>
        /// Author: ThanhNd
        /// CreatedTime : 03/08/2023
        /// Description : Lấy danh sách NTD (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        Task<object> ListEmployerAggregates(DTParameters parameters);
        /// <summary>
        /// Author: ThanhNd
        /// CreatedTime: 04/08/2023
        /// Description: Đặt lại mật khẩu NTD (dành cho admin)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(Employer obj);
        /// <summary>
        /// Author: ThanhNd
        /// CreatedTime: 04/08/2023
        /// Description: quick Activated (dành cho admin)
        /// </summary>
        /// <param name="id">employerID</param>
        /// <returns></returns>
        Task<bool> QuickActivatedAsync(long id);
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        Task<bool> CheckEmailIsActive(string email);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Desciption: Check job is of employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<bool> PrivacyJob(long employerId, long jobId);
    }
}
