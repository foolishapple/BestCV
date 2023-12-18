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

        Task<BestCVResponse> SignIn(SignInAdminAccountDTO obj);

        Task<BestCVResponse> UpdatePassword(ChangePasswordAdminAccountDTO obj);

        Task<AdminAccount?> DetailSignIn(SignInAdminAccountDTO obj);
        Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email);
        Task<int> CountResetPassword(long AdminAccountId);
        Task<bool> CheckEmailIsActive(string email);
        Task<AdminAccount?> GetByEmailAsync(string email);
        Task<bool> CheckKeyValid(string code, string hash);
        Task<BestCVResponse> ResetNewPassword(string code, string hash, string password);


        Task<bool> IsAdminRole(long id);
    }
}
