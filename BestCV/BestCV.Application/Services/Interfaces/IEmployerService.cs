using BestCV.Application.Models.Candidates;
using BestCV.Application.Models.Employer;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IEmployerService : IServiceQueryBase<long,EmployerSignUpDTO,UpdateEmployerDTO, Employer>
    {
        /// <summary>
        /// author: truongthieuhuyen
        /// created: 26-07-2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<(DionResponse, EmailMessage<EmployerConfirmEmailBody>?)> SignUp(EmployerSignUpDTO obj);

        Task<(DionResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email);

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 29.07.2023
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        Task<DionResponse> CheckVerifyCode(string value, string hashCode);


        /// <summary>
        /// author: truongthieuhuyen
        /// created: 30.07.2023
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        Task<EmployerMeta> GetEmailByVerifyCode(ConfirmEmailEmployerDTO obj);


        Task<(DionResponse, EmailMessage<EmployerConfirmEmailBody>?)> ReSendEmail(string email);


        /// <summary>
        /// author: truongthieuhuyen
        /// created: 29.07.2023
        /// </summary>
        /// <param name="email"></param>
        /// <returns>kích hoạt tài khoản qua email</returns>
        Task ActivateAccount(string email);

        Task<DionResponse> SignInAsync(SignInEmployerDTO obj);
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 28/07/2023
        /// Description : update password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> ChangePassword(long id, ChangePasswordEmployerDTO obj);

        Task<object> ListEmployerAggregates(DTParameters parameter);
        Task<DionResponse> AdminDetailAsync(long id);
        Task<DionResponse> ChangePasswordAdminAsync(ChangePasswordDTO obj);

        Task<DionResponse> QuickActivatedAsync(long id);

        Task<int> CountResetPassword(long employerId);
        Task<bool> CheckEmailIsActive(string email);
        Task<Employer?> GetByEmailAsync(string email);
        Task<bool> CheckKeyValid(string code, string hash);
        Task<DionResponse> ResetNewPassword(string code, string hash, string password);
    }
}
