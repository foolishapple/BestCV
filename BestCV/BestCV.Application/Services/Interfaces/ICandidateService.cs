using BestCV.Application.Models.Candidates;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using Microsoft.AspNetCore.Identity;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using BestCV.Domain.Aggregates.Candidate;
using BestCV.Core.Utilities;
using BestCV.Application.Models.CandidateJobSuggestionSetting;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateService : IServiceQueryBase<long, SignupCandidateDTO, UpdateProfileCandidateDTO, CandidateDTO>
    {
       
        Task<BestCVResponse> UpdatePasswordCandidate(ChangePasswordDTO obj,long userId);
        Task<(BestCVResponse, EmailMessage<CandidateConfirmEmailBody>?)> CandidateSignup (SignupCandidateDTO signupCandidateDTO);

        Task<BestCVResponse> CheckVerifyCode(string value, string hashCode);

        Task ActivateAccount(string email);

        Task<int> CountSendingEmail(long id);

        Task<CandidateMeta> GetEmailByVerifyCode(ConfirmEmailCandidateDTO obj);

        Task<(BestCVResponse, EmailMessage<CandidateConfirmEmailBody>?)> ReSendEmail(string email);
        Task<BestCVResponse> UpdateNotiEmailCandidate(SettingNotiEmailDTO obj);
        Task<BestCVResponse> ListCandidateDetailById(long id);
        Task<BestCVResponse> SignIn(SigninCandidateDTO obj);

        Task<BestCVResponse> UpdateProfileCandidate(UpdateProfileCandidateDTO obj);
        Task<BestCVResponse> UpdateCandidateJobSuggestionSetting(UpdateCandidateJobSuggetionSettingDTO obj);
        Task<BestCVResponse> SignInWithGoole(SignInWithSocialNetworkDTO obj);

        Task<BestCVResponse> SignInWithFacebook(SignInWithSocialNetworkDTO obj);
        Task<BestCVResponse> SignInWithLinkedIn(SignInWithSocialNetworkDTO obj);

        bool EmailValidate(string email);
        Task<object> ListCandidateAggregates(CandidateDTParameters parameters);
        Task<BestCVResponse> QuickActivatedAsync(long id);
        Task<BestCVResponse> ChangePasswordAdminAsync(ChangePasswordDTO obj);
        Task<BestCVResponse> ExportExcel(List<CandidateAggregates> data);
        byte[] DownloadExcel(string fileGuid, string fileName);
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: tạo candidateMeta để tạo actionLink trong email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email);
        /// <summary>
        /// Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: Send email quên mật khẩu cho ứng viên
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="empMeta"></param>
        /// <returns></returns>
        EmailMessage<ForgotPasswordEmailBody> SendEmailForgotPasswordAsync(Candidate candidate, CandidateMeta candidateMeta);
        Task<int> CountResetPassword(long candidateMeta);
        /// <summary>
        /// Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: Check email đã active chưa
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckEmailIsActive(string email);
        /// <summary>
        /// Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: lấy candidate qua email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Candidate?> GetByEmailAsync(string email);
        /// <summary>
        /// Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: CheckKeyValid kiểm tra thời gian của tồn tại của links reset password
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        Task<bool> CheckKeyValid(string code, string hash);
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/8/2023
        /// Description: thay đổi mật khẩu của ứng viên và cập nhật active candidateMeta 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<BestCVResponse> ResetNewPassword(string code, string hash, string password);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check Candidate is active
        /// </summary>
        /// <param name="id">candidate Id</param>
        /// <returns></returns>
        Task<bool> IsActive(long id);
        Task<object> FindCandidateAgrregates(FindCandidateParameters parameters);
    }
}
