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

        Task<(BestCVResponse, EmailMessage<EmployerConfirmEmailBody>?)> SignUp(EmployerSignUpDTO obj);

        Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email);

        Task<BestCVResponse> CheckVerifyCode(string value, string hashCode);



        Task<EmployerMeta> GetEmailByVerifyCode(ConfirmEmailEmployerDTO obj);


        Task<(BestCVResponse, EmailMessage<EmployerConfirmEmailBody>?)> ReSendEmail(string email);


        Task ActivateAccount(string email);

        Task<BestCVResponse> SignInAsync(SignInEmployerDTO obj);

        Task<BestCVResponse> ChangePassword(long id, ChangePasswordEmployerDTO obj);

        Task<object> ListEmployerAggregates(DTParameters parameter);
        Task<BestCVResponse> AdminDetailAsync(long id);
        Task<BestCVResponse> ChangePasswordAdminAsync(ChangePasswordDTO obj);

        Task<BestCVResponse> QuickActivatedAsync(long id);

        Task<int> CountResetPassword(long employerId);
        Task<bool> CheckEmailIsActive(string email);
        Task<Employer?> GetByEmailAsync(string email);
        Task<bool> CheckKeyValid(string code, string hash);
        Task<BestCVResponse> ResetNewPassword(string code, string hash, string password);
    }
}
