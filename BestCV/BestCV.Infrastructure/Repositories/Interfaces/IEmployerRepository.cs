using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerRepository : IRepositoryBaseAsync<Employer, long, JobiContext>
    {

        /// <param name="username"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra username đã tồn tại hay chưa</returns>
        Task<bool> UsernameExisted(string username);


        /// <param name="email"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra email đã tồn tại hay chưa</returns>
        Task<bool> EmailExisted(string email);


        /// <param name="phone"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra số điện thoại đã tồn tại hay chưa</returns>
        Task<bool> PhonedExisted(string phone);


        /// <param name="skypeAccount"></param>
        /// <returns>trả về đúng/sai của logic kiểm tra tài khoản skype đã tồn tại hay chưa</returns>
        Task<bool> SkypeAccountExisted(string skypeAccount);

        Task<Employer> FindByEmail(string email);

        /// Description: Lấy thông tin nhà tuyển dụng theo Email

        /// <param name="email">email</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        Task<Employer?> GetByEmailAsync(string email);


        /// Description: Lấy thông tin nhà tuyển dụng theo số điện thoại

        /// <param name="email">phone</param>
        /// <returns>Thông tin nhà tuyển dụng</returns>
        Task<Employer?> GetByPhoneAsync(string phone);
        /// <summary>
        /// Description : Lấy danh sách NTD (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        Task<object> ListEmployerAggregates(DTParameters parameters);
        /// <summary>
        /// Description: Đặt lại mật khẩu NTD (dành cho admin)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(Employer obj);
        /// <summary>
        /// Description: quick Activated (dành cho admin)
        /// </summary>
        /// <param name="id">employerID</param>
        /// <returns></returns>
        Task<bool> QuickActivatedAsync(long id);
        /// <summary>
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns>trả về true/false</returns>
        Task<bool> CheckEmailIsActive(string email);
        /// <summary>
        /// Desciption: Check job is of employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<bool> PrivacyJob(long employerId, long jobId);
    }
}
