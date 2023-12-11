using BestCV.Application.Models.AdminAccounts;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IAdminAccountService : IServiceQueryBase<long, InsertAdminAccountDTO, UpdateAdminAccountDTO,AdminAccountDTO>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Sign in admin account
        /// </summary>
        /// <param name="obj">Sign in admin account object</param>
        /// <returns></returns>
        Task<DionResponse> SignIn(SignInAdminAccountDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: Update new pass word to admin accont
        /// </summary>
        /// <param name="obj">change password admin acount object</param>
        /// <returns></returns>
        Task<DionResponse> UpdatePassword(ChangePasswordAdminAccountDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 09/08/2023
        /// Description: Detail SignIn
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<AdminAccount?> DetailSignIn(SignInAdminAccountDTO obj);
        Task<(DionResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email);
        Task<int> CountResetPassword(long AdminAccountId);
        Task<bool> CheckEmailIsActive(string email);
        Task<AdminAccount?> GetByEmailAsync(string email);
        Task<bool> CheckKeyValid(string code, string hash);
        Task<DionResponse> ResetNewPassword(string code, string hash, string password);

        /// <summary>
        /// Author : DucNN
        /// CreatedTime : 11/8/2023
        /// Description: check tài khoản có phải admin không ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsAdminRole(long id);
    }
}
