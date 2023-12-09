using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Candidate;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository : IRepositoryBaseAsync<Candidate, long, JobiContext>
    {
        public Task<bool> IsEmailExist(string email);
        public Task<bool> IsFullNameExist(string userName);
        public Task<bool> IsPhoneExist(string phone);


        public Task<Candidate> SignIn(Candidate obj);


        /// Description: Lấy thông tin ứng viên theo Email
        /// <param name="email">email</param>
        /// <returns>Thông tin ứng viên</returns>
        Task<Candidate?> GetByEmailAsync(string email);


        /// Description: Lấy thông tin ứng viên theo phone
        /// <param name="phone">email</param>
        /// <returns>Thông tin ứng viên</returns>
        Task<Candidate?> GetByPhoneAsync(string phone);
        Task<Candidate> FindByEmail(string email);



        /// Description : Update GoogleId
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateGoogleId(Candidate obj);


        /// Description : Check facebookId
        /// <param name="facebookId"></param>
        /// <returns></returns>
        Task<Candidate> CheckCandidateByFacebookId(string facebookId);

        /// Description : Check linkedinId
        /// <param name="linkedinId"></param>
        /// <returns></returns>
        Task<Candidate> CheckCandidateByLinkedinId(string linkedinId);

        /// Description : Update facebookId
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateFacebookId(Candidate obj);

        /// Description : Update linkedinId
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateLinkedinId(Candidate obj);
        //Task ActivateAccount(string email);
        public Task<object> ListCandidateAggregates(CandidateDTParameters parameters);
        Task<bool> QuickActivatedAsync(long id);
        Task<bool> ChangePasswordAsync(Candidate obj);

        /// Description: check email đã đăng ký chưa?
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckEmailIsActive(string email);

        /// Description : List Candidate agrreegate (employer find CV)
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> FindCandidateAgrregates(FindCandidateParameters parameters);
    }
}
