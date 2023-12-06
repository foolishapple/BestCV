using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.Candidate;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository : IRepositoryBaseAsync<Candidate, long, JobiContext>
    {
        public Task<bool> IsEmailExist(string email);
        public Task<bool> IsFullNameExist(string userName);
        public Task<bool> IsPhoneExist(string phone);

        /// <summary>
        /// Author: Nam Anh
        /// Created:26/7/2023
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<Candidate> SignIn(Candidate obj);

        /// <summary>
        /// Author: Nam Anh
        /// CreatedDate: 1/08/2023
        /// Description: Lấy thông tin ứng viên theo Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Thông tin ứng viên</returns>
        Task<Candidate?> GetByEmailAsync(string email);

        /// <summary>
        /// Author: Nam Anh
        /// CreatedDate: 1/08/2023
        /// Description: Lấy thông tin ứng viên theo phone
        /// </summary>
        /// <param name="phone">email</param>
        /// <returns>Thông tin ứng viên</returns>
        Task<Candidate?> GetByPhoneAsync(string phone);
        Task<Candidate> FindByEmail(string email);

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Update GoogleId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateGoogleId(Candidate obj);

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Check facebookId
        /// </summary>
        /// <param name="facebookId"></param>
        /// <returns></returns>
        Task<Candidate> CheckCandidateByFacebookId(string facebookId);
        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Check linkedinId
        /// </summary>
        /// <param name="linkedinId"></param>
        /// <returns></returns>
        Task<Candidate> CheckCandidateByLinkedinId(string linkedinId);
        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Update facebookId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateFacebookId(Candidate obj);
        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 02/08/2023
        /// Description : Update linkedinId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateLinkedinId(Candidate obj);
        //Task ActivateAccount(string email);
        public Task<object> ListCandidateAggregates(CandidateDTParameters parameters);
        Task<bool> QuickActivatedAsync(long id);
        Task<bool> ChangePasswordAsync(Candidate obj);
        /// <summary>
        /// Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: check email đã đăng ký chưa?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckEmailIsActive(string email);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 25/09/2023
        /// Description : List Candidate agrreegate (employer find CV)
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> FindCandidateAgrregates(FindCandidateParameters parameters);
    }
}
